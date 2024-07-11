namespace IWantApp.Endpoints.Orders;

public class OrderGeyById
{
    public static string Template => "/order/{id:guid}";
    public static string[] Methods => [HttpMethod.Get.ToString()];
    public static Delegate Handle => Action;

    public static async Task<IResult> Action([FromRoute] Guid id, UserInfoService userInfo,
        UserManager<IdentityUser> userManager, AppDbContext context)
    {
        var order = await context.Orders
            .AsNoTracking()
            .Include(o => o.Products)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (userInfo.Id != order!.ClientId && !userInfo.IsEmployee)
            return Results.Forbid();

        if (order is null)
            return Results.NotFound();

        var client = await userManager.FindByIdAsync(order.ClientId);

        var response = new OrderResponse(
            order.Id,
            client?.Email!, 
            order.Products.Select(p => new OrderProduct(p.Id, p.Name)).ToList(), 
            order.DeliveryAddress, order.Total);

        return Results.Ok(response);
    }
}
