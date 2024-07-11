namespace IWantApp.Extensions;

public static class EndpointExtension
{
    public static void AddEndpoints(this WebApplication app)
    {
        CategoryEndpoints(app);
        EmployeeEndpoints(app);
        ClientEndpoints(app);
        ProductEndpoints(app);
        OrderEndpoints(app);
        TokenEndpoints(app);
    }

    public static void CategoryEndpoints(WebApplication app)
    {
        app.MapMethods(CategoryPost.Template, CategoryPost.Methods, CategoryPost.Handle);
        app.MapMethods(CategoryGetAll.Template, CategoryGetAll.Methods, CategoryGetAll.Handle);
        app.MapMethods(CategoryGetById.Template, CategoryGetById.Methods, CategoryGetById.Handle);
        app.MapMethods(CategoryPut.Template, CategoryPut.Methods, CategoryPut.Handle);
        app.MapMethods(CategoryDelete.Template, CategoryDelete.Methods, CategoryDelete.Handle);
    }

    public static void EmployeeEndpoints(WebApplication app)
    {
        app.MapMethods(EmployeePost.Template, EmployeePost.Methods, EmployeePost.Handle);
        app.MapMethods(EmployeeGetAll.Template, EmployeeGetAll.Methods, EmployeeGetAll.Handle);
    }

    public static void ClientEndpoints(WebApplication app)
    {
        app.MapMethods(ClientPost.Template, ClientPost.Methods, ClientPost.Handle);
        app.MapMethods(ClientLogged.Template, ClientLogged.Methods, ClientLogged.Handle);
    }

    public static void ProductEndpoints(WebApplication app)
    {
        app.MapMethods(ProductPost.Template, ProductPost.Methods, ProductPost.Handle);
        app.MapMethods(ProductGetAll.Template, ProductGetAll.Methods, ProductGetAll.Handle);
        app.MapMethods(ProductGetById.Template, ProductGetById.Methods, ProductGetById.Handle);
        app.MapMethods(ProductGetShowcase.Template, ProductGetShowcase.Methods, ProductGetShowcase.Handle);
        app.MapMethods(ProductSoldGet.Template, ProductSoldGet.Methods, ProductSoldGet.Handle);
    }

    public static void OrderEndpoints(WebApplication app)
    {
        app.MapMethods(OrderPost.Template, OrderPost.Methods, OrderPost.Handle);
        app.MapMethods(OrderGeyById.Template, OrderGeyById.Methods, OrderGeyById.Handle);
    }

    public static void TokenEndpoints(WebApplication app)
    {
        app.MapMethods(TokenPost.Template, TokenPost.Methods, TokenPost.Handle);
    }
}
