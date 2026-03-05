
using Application.Abstractions.Messaging;
using Application.Reports.Admin.Get;
using SharedKernel;
using Web.Api.Endpoints.Requests;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Reports.Admin;

internal sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("admin/reports", async (
            [AsParameters]GetRequest request,
            IQueryHandler<GetReportQuery, List<ReportResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetReportQuery(
                                    request.sort,
                                    request.pageSize,
                                    request.pageoffset
                                    );

            Result<List<ReportResponse>> result = await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Admin)
        //.RequireAuthorization(
        //    AuthRequirements.RESPONDER_PNP,
        //    AuthRequirements.RESPONDER_BFP,
        //    AuthRequirements.RESPONDER_CTMO,
        //    AuthRequirements.ADMIN)
        ;
    }
}
