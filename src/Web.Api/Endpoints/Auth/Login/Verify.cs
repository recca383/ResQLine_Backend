
using Application.Abstractions.Messaging;
using Application.Otps.Login.Verify;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Otps.Login;

public class Verify : IEndpoint
{
    public sealed record Request(string mobileNumber, string otp);
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("otp/login/verify", async (
            Request request,
            ICommandHandler<VerifyLoginOtpCommand, string> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new VerifyLoginOtpCommand(request.mobileNumber, request.otp);

            Result<string> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Otp);
    }
}
