namespace IWantApp.Endpoints.Empoyee;

public class EmployeeGetAll
{
    public static string Template => "/employee";
    public static string[] Methods => [HttpMethod.Get.ToString()];
    public static Delegate Handle => Action;

    [Authorize(Policy = nameof(TokenPolicies.Employee005Policy))]
    public static async Task<IResult> Action(int? page, int? rows, QueryAllUserWithClaimName query)
    {
        var pagination = new Pagination(page, rows);

        if (!pagination.IsValid)
            return Results.ValidationProblem(pagination.Notifications.ConvertToProblemsDetail());

        var employees = await query.Execute(page!.Value, rows!.Value);

        if (employees?.Count() == 0)
            return Results.NoContent();

        return Results.Ok(employees);
    }
}
