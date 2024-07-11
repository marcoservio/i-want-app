namespace IWantApp.Endpoints.Clients;

public class ClientPost
{
    public static string Template => "/client";
    public static string[] Methods => [HttpMethod.Post.ToString()];
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static async Task<IResult> Action(ClientRequest request, UserCreatorService userCreator)
    {
        var userClaims = new List<Claim>
        {
            new(nameof(ClaimsUser.Cpf), request.Cpf),
            new(nameof(ClaimsUser.Name), request.Name)
        };

        (var result, var newUserId) = await userCreator.Create(request.Email, request.Password, userClaims);

        if (!result.Succeeded)
            return Results.ValidationProblem(result.Errors.ConvertToProblemsDetail());     

        return Results.Created($"/client/{newUserId}", newUserId);
    }
}
