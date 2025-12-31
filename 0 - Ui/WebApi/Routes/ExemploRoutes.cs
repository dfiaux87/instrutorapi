namespace WebApi.Routes;

public static class ExemploRoutes
{
    public static void MapExemploEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/exemplo", () => "Rota de exemplo");
    }
}
