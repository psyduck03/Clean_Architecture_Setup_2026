
namespace CleanArch26.API.Modules;
public static class RouteRegistrar
{
    public static void RegisterRoutes(this IEndpointRouteBuilder app)
    {
        app.RegisterAuthRoutes();
        app.RegisterEmployeeRoutes();
    }
}