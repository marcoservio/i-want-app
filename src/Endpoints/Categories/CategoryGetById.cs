namespace IWantApp.Endpoints.Categories;

public class CategoryGetById
{
    public static string Template => "/category/{id:guid}";
    public static string[] Methods => [HttpMethod.Get.ToString()];
    public static Delegate Handle => Action;

    public static IResult Action([FromRoute] Guid id, AppDbContext context)
    {
        var category = context.Categories.FirstOrDefault(c => c.Id == id);

        if (category != null)
        {
            var response = new CategoryResponse(category.Id, category.Name, category.Active);

            return Results.Ok(response);
        }

        return Results.NotFound();
    }
}
