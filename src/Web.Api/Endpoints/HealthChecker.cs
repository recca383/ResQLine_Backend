
namespace Web.Api.Endpoints;

public class HealthChecker : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/", () => "API is running!");
    }
}
