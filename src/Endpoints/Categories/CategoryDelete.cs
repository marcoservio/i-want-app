namespace IWantApp.Endpoints.Categories;

public class CategoryDelete
{
    public static string Template => "/category/{id}";
    public static string[] Methods => [HttpMethod.Delete.ToString()];
    public static Delegate Handle => Action;

    [Authorize(Policy = nameof(TokenPolicies.EmployeePolicy))]
    public static async Task<IResult> Action([FromRoute]Guid id, AppDbContext context)
    {
        var category = context.Categories.FirstOrDefault(c => c.Id == id); 

        context.Categories.Remove(category!);
        await context.SaveChangesAsync();

        return Results.NoContent();
    }
}
