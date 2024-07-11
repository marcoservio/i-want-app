namespace IWantApp.Endpoints.Categories;

public class ProductGetById
{
    public static string Template => "/product/{id:guid}";
    public static string[] Methods => [HttpMethod.Get.ToString()];
    public static Delegate Handle => Action;

    [Authorize(Policy = nameof(TokenPolicies.EmployeePolicy))]
    public static async Task<IResult> Action([FromRoute] Guid id, AppDbContext context)
    {
        var product = await context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);

        if (product != null)
        {
            var response = new ProductResponse(product.Id, product.Name, product.Category?.Name!, product.Description, product.HasStock, product.Price, product.Active);

            return Results.Ok(response);
        } 

        return Results.NotFound();
    }
}
