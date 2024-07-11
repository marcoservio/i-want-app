namespace IWantApp.Endpoints.Categories;

public class CategoryPost
{
    public static string Template => "/category";
    public static string[] Methods => [HttpMethod.Post.ToString()];
    public static Delegate Handle => Action;

    [Authorize(Policy = nameof(TokenPolicies.EmployeePolicy))]
    public static async Task<IResult> Action(CategoryRequest request, HttpContext http, AppDbContext context)
    {
        var userId = http.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        var category = new Category(request.Name, userId, userId);

        if (!category.IsValid)
            return Results.ValidationProblem(category.Notifications.ConvertToProblemsDetail());

        await context.Categories.AddAsync(category);
        await context.SaveChangesAsync();

        return Results.Created($"/category/{category.Id}", category.Id);
    }
}
