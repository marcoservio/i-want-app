using System.Text.Json;

namespace IWantApp.Extensions;

public static class FilterExceptionExtension
{
    public static void AddFilterException(this WebApplication app)
    {
        app.UseExceptionHandler("/error");
        app.Map("/error", (HttpContext http) =>
        {
            var error = http.Features?.Get<IExceptionHandlerFeature>()?.Error;

            if (error != null)
            {
                if (error is SqlException)
                    return Results.Problem(title: "Database out", statusCode: (int)HttpStatusCode.InternalServerError);
                else if (error is BadHttpRequestException)
                    return Results.Problem(title: "Error to convert data to other type. See all the information sent", statusCode: (int)HttpStatusCode.InternalServerError);
            }

            return Results.Problem(title: "An error ocurred", statusCode: (int)HttpStatusCode.InternalServerError);
        });
    }
}
