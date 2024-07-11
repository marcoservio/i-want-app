namespace IWantApp.Extensions;

public static class TokenExtension
{
    public static void AddJwtToken(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
            .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser()
            .Build();
            options.AddPolicy(nameof(TokenPolicies.EmployeePolicy), 
                p => p.RequireAuthenticatedUser().RequireClaim(nameof(ClaimsUser.EmployeeCode)));
            options.AddPolicy(nameof(TokenPolicies.Employee005Policy),
                p => p.RequireAuthenticatedUser().RequireClaim(nameof(ClaimsUser.EmployeeCode), "005"));
            options.AddPolicy(nameof(TokenPolicies.CpfPolicy),
                p => p.RequireAuthenticatedUser().RequireClaim(nameof(ClaimsUser.Cpf)));
        });
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateActor = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = configuration["JwtBearerTokenSettings:Issuer"],
                ValidAudience = configuration["JwtBearerTokenSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtBearerTokenSettings:SecretKey"]!))
            };
        });
    }
}
