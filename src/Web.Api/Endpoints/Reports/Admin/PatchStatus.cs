
using Application.Abstractions.Messaging;
using Application.Reports.Complete;
using Domain.Reports;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Reports.Admin;

internal sealed class PatchStatus : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("admin/reports/{id:guid}/status", async (
            Guid id,
            [FromBody]Status status,
            ICommandHandler<PatchStatusCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new PatchStatusCommand(id,status);

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
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
