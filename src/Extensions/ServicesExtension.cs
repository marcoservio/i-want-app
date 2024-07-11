namespace IWantApp.Extensions;

public static class ServicesExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddScoped<QueryAllUserWithClaimName>();
        services.AddScoped<QueryAllProductsSold>();

        services.AddScoped<UserCreatorService>();
        services.AddScoped<UserInfoService>();
    }
}
