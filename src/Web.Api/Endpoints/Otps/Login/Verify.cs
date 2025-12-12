
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
            ICommandHandler<VerifyLoginOtpCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new VerifyLoginOtpCommand(request.mobileNumber, request.otp);

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(()=>Results.Accepted(), CustomResults.Problem);
        })
        .WithTags(Tags.Otp);
    }
}
