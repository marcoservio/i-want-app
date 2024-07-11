namespace IWantApp.Extensions;

public static class ContextExtension
{
    public static void AddContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSqlServer<AppDbContext>(configuration.GetConnectionString("SqlServer"));
        services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
    }
}
