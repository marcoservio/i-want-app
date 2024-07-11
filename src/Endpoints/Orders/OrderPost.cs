namespace IWantApp.Endpoints.Orders;

public class OrderPost
{
    public static string Template => "/order";
    public static string[] Methods => [HttpMethod.Post.ToString()];
    public static Delegate Handle => Action;

    [Authorize(Policy = nameof(TokenPolicies.CpfPolicy))]
    public static async Task<IResult> Action(OrderRequest request, HttpContext http, AppDbContext context)
    {
        var clientId = http.User.Claims
            .FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))!.Value;
        var clientName = http.User.Claims
            .FirstOrDefault(c => c.Type.Equals(nameof(ClaimsUser.Name)))!.Value;

        List<Product>? productsFound = null;

        if(request.ProductIds?.Count > 0)
            productsFound = [.. context.Products.Where(p => request.ProductIds.Contains(p.Id))];

        var order = new Order(clientId, clientName, productsFound!, request.DeliveryAddress);

        if (!order.IsValid)
            return Results.ValidationProblem(order.Notifications.ConvertToProblemsDetail());

        await context.Orders.AddAsync(order);
        await context.SaveChangesAsync();

        return Results.Created($"/order/{order.Id}", order.Id);
    }
}
