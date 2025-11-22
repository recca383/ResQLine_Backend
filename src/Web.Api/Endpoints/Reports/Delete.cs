using Application.Abstractions.Messaging;
using Application.Reports.Delete;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Reports;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("reports/{id:guid}", async (
            Guid id,
            ICommandHandler<DeleteReportCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new DeleteReportCommand(id);

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Reports)
        .RequireAuthorization();
    }
}
