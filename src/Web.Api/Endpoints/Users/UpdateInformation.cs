
using Application.Abstractions.Messaging;
using Application.Users.UpdateInformation;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Users;

internal sealed class UpdateInformation : IEndpoint
{
    private sealed record Request(string FirstName, string LastName, string Username);
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("users/updateinformation", async (
            Request request,
            ICommandHandler<UpdateInformationCommand, UserResponse> handler,
            CancellationToken cancellationToken
            ) =>
        {
            var command = new UpdateInformationCommand(
                FirstName: request.FirstName,
                LastName: request.LastName,
                UserName: request.Username
                );

            Result<UserResponse> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);

        })
        .WithTags(Tags.Users)
        .RequireAuthorization();
    }
}
