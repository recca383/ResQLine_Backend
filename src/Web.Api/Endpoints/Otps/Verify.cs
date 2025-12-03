
using Application.Abstractions.Messaging;
using Application.Otps.Verify;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Otps;

internal sealed class Verify : IEndpoint
{
    public sealed record Request(string mobileNumber, string Otp);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("otp/verify", async (
            Request request,
            ICommandHandler<VerifyOtpCommand, string> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new VerifyOtpCommand(request.mobileNumber, request.Otp);

            Result<string> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Otp);
            
    }
}
