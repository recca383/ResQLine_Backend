using Application.Abstractions.Hubs;
using Web.Api.Endpoints;

namespace Web.Api.Hubs;

public class Notification : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapHub<NotificationHub>("/hub/Notification");
    }
}
