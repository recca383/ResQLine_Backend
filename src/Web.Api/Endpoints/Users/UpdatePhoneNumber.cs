
using Application.Abstractions.Messaging;
using Application.Users.UpdatePhoneNumber;
using Domain.Users;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Users;

internal sealed class UpdatePhoneNumber : IEndpoint
{
    private sealed record Request(string mobileNumber);
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("users/updatephonenumber", async (
            Request request,
            ICommandHandler<UpdatePhoneNumberCommand, UserResponse> handler,
            CancellationToken cancellationToken
            ) =>
        {
            var command = new UpdatePhoneNumberCommand(request.mobileNumber);

            Result<UserResponse> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users)
        .RequireAuthorization();
    }
}
