namespace IWantApp.Endpoints.Categories;

public class ProductSoldGet
{
    public static string Template => "/product/sold";
    public static string[] Methods => [HttpMethod.Get.ToString()];
    public static Delegate Handle => Action;

    [Authorize(Policy = nameof(TokenPolicies.EmployeePolicy))]
    public static async Task<IResult> Action(QueryAllProductsSold query)
    {
        var result = await query.Execute();

        if(result?.Count() > 0)
            return Results.Ok(result);

        return Results.NoContent();
    }
}
