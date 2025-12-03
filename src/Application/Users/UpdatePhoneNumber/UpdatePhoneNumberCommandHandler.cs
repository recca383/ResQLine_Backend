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

namespace Application.Users.UpdatePhoneNumber;
internal sealed class UpdatePhoneNumberCommandHandler(
    IApplicationDbContext context,
    IUserContext userContext)
    : ICommandHandler<UpdatePhoneNumberCommand, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(UpdatePhoneNumberCommand command, CancellationToken cancellationToken)
    {
        User? user = await context.Users
            .SingleOrDefaultAsync(u => u.Id == userContext.UserId, cancellationToken);

        if (user == null)
        {
            return Result.Failure<UserResponse>(UserErrors.NotFound(userContext.UserId));
        }

        if (user.MobileNumber == command.MobileNumber)
        {
            return Result.Failure<UserResponse>(UserErrors.AlreadySet(command.MobileNumber));
        }

        user.MobileNumber = command.MobileNumber;

        context.Users.Update(user);

        await context.SaveChangesAsync(cancellationToken);

        var response = new UserResponse()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            MobileNumber = user.MobileNumber
        };

        return Result.Success(response);
    }
}
