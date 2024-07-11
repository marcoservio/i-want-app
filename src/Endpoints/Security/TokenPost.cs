namespace IWantApp.Endpoints.Security;

public class TokenPost
{
    public static string Template => "/token";
    public static string[] Methods => [HttpMethod.Post.ToString()];
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static async Task<IResult> Action(LoginRequest request, UserManager<IdentityUser> userManager, 
        IConfiguration configuration, ILogger<TokenPost> log, IWebHostEnvironment environment)
    {
        log.LogInformation("Getting Token");
        log.LogWarning("Warning");
        log.LogError("Error");

        var user = await userManager.FindByEmailAsync(request.Email);

        if (user == null)
            return Results.BadRequest();

        if (!await userManager.CheckPasswordAsync(user, request.Password))
            return Results.BadRequest();

        var claims = await userManager.GetClaimsAsync(user);
        var subject = new ClaimsIdentity(
        [
            new(ClaimTypes.Email, request.Email),
            new(ClaimTypes.NameIdentifier, user.Id),
        ]);
        subject.AddClaims(claims);

        var key = Encoding.ASCII.GetBytes(configuration["JwtBearerTokenSettings:SecretKey"]!);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = subject,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Audience = configuration["JwtBearerTokenSettings:Audience"],
            Issuer = configuration["JwtBearerTokenSettings:Issuer"],
            Expires = environment.IsDevelopment() || environment.IsStaging() ? 
                DateTime.UtcNow.AddYears(1) : DateTime.UtcNow.AddMinutes(Convert.ToInt32(configuration["JwtBearerTokenSettings:ExpiryTimeInSeconds"])),
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return Results.Ok(new
        {
            token = tokenHandler.WriteToken(token)
        });
    }
}
