namespace IWantApp.Endpoints.Categories;

public class ProductGetAll
{
    public static string Template => "/product";
    public static string[] Methods => [HttpMethod.Get.ToString()];
    public static Delegate Handle => Action;

    [Authorize(Policy = nameof(TokenPolicies.EmployeePolicy))]
    public static async Task<IResult> Action(AppDbContext context)
    {
        var products = await context.Products.Include(p => p.Category).OrderBy(p => p.Name).ToListAsync();

        if (products?.Count > 0)
        {
            var response = products.Select(p => new ProductResponse(p.Id, p.Name, p.Category?.Name!, p.Description, p.HasStock, p.Price, p.Active)).ToList();

            return Results.Ok(response);
        }

        return Results.NoContent();
    }
}
