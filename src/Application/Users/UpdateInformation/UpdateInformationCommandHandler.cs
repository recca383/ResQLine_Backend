using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Users.UpdateInformation;
internal sealed class UpdateInformationCommandHandler(
    IApplicationDbContext context,
    IUserContext userContext
    )
    :ICommandHandler<UpdateInformationCommand, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(UpdateInformationCommand command, CancellationToken cancellationToken)
    {
        User? user = await context.Users
            .SingleOrDefaultAsync(u => u.Id == userContext.UserId,
                                    cancellationToken);

        if (user == null)
        {
            Result.Failure<UserResponse>(UserErrors.NotFound(userContext.UserId));
        }

        user!.FirstName = command.FirstName;
        user!.LastName = command.LastName;
        user!.UserName = command.UserName;

        context.Users.Update(user);

        await context.SaveChangesAsync(cancellationToken);

        var response = new UserResponse()
        { 
            FirstName = command.FirstName,
            LastName = command.LastName,
            UserName = command.UserName,
        };

        return Result.Success<UserResponse>(response);
    }
}
