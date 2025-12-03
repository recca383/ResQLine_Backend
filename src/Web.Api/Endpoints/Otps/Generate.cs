using Application.Abstractions.Messaging;
using Application.Otps.Generate;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Otps;

internal sealed class Generate : IEndpoint
{
    public sealed record Request(string mobileNumber);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("otp/generate", async (
            Request request,
            ICommandHandler<GenerateOtpCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new GenerateOtpCommand(request.mobileNumber);

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Otp);
    }
}
