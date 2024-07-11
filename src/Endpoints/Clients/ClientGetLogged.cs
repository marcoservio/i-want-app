namespace IWantApp.Endpoints.Clients;

public class ClientLogged
{
    public static string Template => "/client";
    public static string[] Methods => [HttpMethod.Get.ToString()];
    public static Delegate Handle => Action;
    
    public static IResult Action(HttpContext http)
    {
        var user = http.User;

        if (user is null)
            return Results.NotFound();

        var result = new
        {
            Id = user.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))!.Value,
            Name = user.Claims.FirstOrDefault(c => c.Type.Equals(nameof(ClaimsUser.Name)))!.Value,
            Cpf = user.Claims.FirstOrDefault(c => c.Type.Equals(nameof(ClaimsUser.Cpf)))!.Value,
        };

        return Results.Ok(result);
    }
}
