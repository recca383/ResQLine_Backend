
using Application.Abstractions.Messaging;
using Application.Otps.Login.Send;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Otps.Login;

internal sealed class Send : IEndpoint
{
    public sealed record Request(string mobileNumber);
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("otp/login/send", async (
            Request request,
            ICommandHandler<SendLoginOtpCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new SendLoginOtpCommand(request.mobileNumber);

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Otp);
    }
}
