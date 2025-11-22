using Application.Abstractions.Messaging;
using Application.Reports.Complete;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Reports;

internal sealed class Complete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("reports/{id:guid}/complete", async (
            Guid id,
            ICommandHandler<ResolveReportCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new ResolveReportCommand(id);

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Reports)
        .RequireAuthorization();
    }
}
