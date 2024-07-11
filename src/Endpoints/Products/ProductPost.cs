namespace IWantApp.Endpoints.Categories;

public class ProductPost
{
    public static string Template => "/product";
    public static string[] Methods => [HttpMethod.Post.ToString()];
    public static Delegate Handle => Action;

    [Authorize(Policy = nameof(TokenPolicies.EmployeePolicy))]
    public static async Task<IResult> Action(ProductRequest request, HttpContext http, AppDbContext context)
    {
        var userId = http.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == request.CategoryId);
        var product = new Product(request.Name, category!, request.Description, request.HasStock, request.Price, userId);

        if (!product.IsValid)
            return Results.ValidationProblem(product.Notifications.ConvertToProblemsDetail());

        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();

        return Results.Created($"/product/{product.Id}", product.Id);
    }
}
