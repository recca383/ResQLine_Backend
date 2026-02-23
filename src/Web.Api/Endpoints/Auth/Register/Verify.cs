
using Application.Abstractions.Messaging;
using Application.Otps.Register.Verify;
using Application.Users.Register;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Otps.Register;

internal sealed class Verify : IEndpoint
{
    public sealed record Request(
        string MobileNumber,
        string FirstName,
        string LastName,
        string Otp);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("otp/register/verify", async (
            Request request,
            ICommandHandler<VerifyRegisterOtpCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new VerifyRegisterOtpCommand(
                request.MobileNumber,
                request.FirstName,
                request.LastName,
                request.Otp);

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(()=> Results.Accepted(), CustomResults.Problem);
        })
        .WithTags(Tags.Otp);
    }
}
