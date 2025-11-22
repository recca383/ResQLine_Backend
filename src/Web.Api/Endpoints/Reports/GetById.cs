using Application.Abstractions.Messaging;
using Application.Reports.GetById;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Reports;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("reports/{id:guid}", async (
            Guid id,
            IQueryHandler<GetReportByIdQuery, ReportResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new GetReportByIdQuery(id);

            Result<ReportResponse> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Reports)
        .RequireAuthorization();
    }
}
