namespace IWantApp.Endpoints.Empoyee;

public class EmployeePost
{
    public static string Template => "/employee";
    public static string[] Methods => [HttpMethod.Post.ToString()];
    public static Delegate Handle => Action;

    [Authorize(Policy = nameof(TokenPolicies.EmployeePolicy))]
    public static async Task<IResult> Action(EmployeeRequest request, UserInfoService userInfo, UserCreatorService userCreator)
    {
        var userClaims = new List<Claim>
        {
            new(nameof(ClaimsUser.EmployeeCode), request.EmployeeCode),
            new(nameof(ClaimsUser.Name), request.Name),
            new(nameof(ClaimsUser.CreatedBy), userInfo.Id)
        };

        (var result, var newUserId) = await userCreator.Create(request.Email, request.Password, userClaims);

        if (!result.Succeeded)
            return Results.ValidationProblem(result.Errors.ConvertToProblemsDetail());  

        return Results.Created($"/employee/{newUserId}", newUserId);
    }
}
