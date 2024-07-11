namespace IWantApp.Infrastructure.Data;

public class QueryAllUserWithClaimName(IConfiguration configuration)
{
    private readonly IConfiguration _configuration = configuration;

    public async Task<IEnumerable<EmployeeResponse>> Execute(int page, int rows)
    {
        var db = new SqlConnection(_configuration.GetConnectionString("SqlServer"));

        var query = @"SELECT Email, ClaimValue as Name
                    FROM AspNetUsers u INNER JOIN AspNetUserClaims c
                    ON u.id = c.UserId
                    AND c.ClaimType = 'Name'
                    ORDER BY Name
                    OFFSET (@page - 1) * @rows ROWS FETCH NEXT @rows ROWS ONLY;";

        return await db.QueryAsync<EmployeeResponse>(query, new { page, rows });
    }
}
