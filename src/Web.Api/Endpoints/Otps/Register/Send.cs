
using Application.Abstractions.Messaging;
using Application.Otps.Register.Send;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Otps.Register;

public class Send : IEndpoint
{
    public sealed record Request(string mobileNumber);
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("otp/register/send", async (
            Request request,
            ICommandHandler<SendRegisterOtpCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new SendRegisterOtpCommand(request.mobileNumber);

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Otp);
    }
}
