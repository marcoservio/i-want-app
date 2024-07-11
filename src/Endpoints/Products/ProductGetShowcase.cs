namespace IWantApp.Endpoints.Categories;

public class ProductGetShowcase
{
    public static string Template => "/product/showcase";
    public static string[] Methods => [HttpMethod.Get.ToString()];
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static async Task<IResult> Action(AppDbContext context, int page = 1, int rows = 10, string orderBy = "name")
    {
        if (rows > 10)
            return Results.Problem(title: "Rows with max 10", statusCode: (int)HttpStatusCode.BadRequest);

        var queryBase = context.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .Where(p => p.HasStock && p.Category!.Active);

        if (orderBy.Equals("name"))
            queryBase = queryBase.OrderBy(p => p.Name);
        else if (orderBy.Equals("price"))
            queryBase = queryBase.OrderBy(p => p.Price);
        else
            return Results.Problem(title: "Order only by 'price' or 'name'", statusCode: (int)HttpStatusCode.BadRequest);

        var queryFilter = queryBase.Skip((page - 1) * rows).Take(rows);        

        var products = await queryFilter.ToListAsync();

        if (products?.Count > 0)
        {
            var response = products.Select(p => new ProductResponse(p.Id, p.Name, p.Category?.Name!, p.Description, p.HasStock, p.Price, p.Active)).ToList();

            return Results.Ok(response);
        }

        return Results.NoContent();
    }
}
