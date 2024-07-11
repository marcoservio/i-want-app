namespace IWantApp.Infrastructure.Data;

public class QueryAllProductsSold(IConfiguration configuration)
{
    private readonly IConfiguration _configuration = configuration;

    public async Task<IEnumerable<ProductSoldReportResponse>> Execute()
    {
        var db = new SqlConnection(_configuration.GetConnectionString("SqlServer"));

        var query = @"
                    SELECT p.Id, p.Name, COUNT(*) as Amount
                    FROM Orders o
                    INNER JOIN OrderProducts op ON o.Id = op.OrdersId
                    INNER JOIN Products p ON p.Id = op.ProductsId
                    GROUP BY p.id, p.Name
                    ORDER BY Amount DESC;";

        return await db.QueryAsync<ProductSoldReportResponse>(query);
    }
}
