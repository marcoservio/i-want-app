namespace IWantApp.Endpoints.Categories;

public class CategoryPut
{
    public static string Template => "/category/{id:guid}";
    public static string[] Methods => [HttpMethod.Put.ToString()];
    public static Delegate Handle => Action;

    [Authorize(Policy = nameof(TokenPolicies.EmployeePolicy))]
    public static async Task<IResult> Action([FromRoute]Guid id, CategoryRequest request, HttpContext http, AppDbContext context)
    {
        var userId = http.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        var category = context.Categories.FirstOrDefault(c => c.Id == id);

        if (category is null)
            return Results.NotFound();

        category.EditInfo(request.Name, request.Active, userId);

        if (!category.IsValid)
            return Results.ValidationProblem(category.Notifications.ConvertToProblemsDetail());

        context.Categories.Update(category);
        await context.SaveChangesAsync();

        return Results.NoContent();
    }
}
