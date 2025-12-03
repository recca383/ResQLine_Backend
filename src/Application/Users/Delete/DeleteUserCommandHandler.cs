using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Users.Delete;
internal sealed class DeleteUserCommandHandler(IApplicationDbContext context)
    : ICommandHandler<DeleteUserCommand>
{
    public async Task<Result> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        User? user = await context.Users
            .SingleOrDefaultAsync(u => u.Id == command.Id, cancellationToken);

        if (user == null)
        {
            return Result.Failure(UserErrors.NotFound(command.Id));
        }

        context.Users.Remove(user);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
