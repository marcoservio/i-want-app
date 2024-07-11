namespace IWantApp.Endpoints.Categories;

public class CategoryGetAll
{
    public static string Template => "/category";
    public static string[] Methods => [HttpMethod.Get.ToString()];
    public static Delegate Handle => Action;

    public static async Task<IResult> Action(AppDbContext context)
    {
        var categories = await context.Categories.ToListAsync();

        if (categories?.Count > 0)
        {
            var response = categories.Select(c => new CategoryResponse(c.Id, c.Name, c.Active)).ToList();

            return Results.Ok(response);
        }

        return Results.NoContent();
    }
}
