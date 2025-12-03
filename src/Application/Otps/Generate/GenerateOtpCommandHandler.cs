using Application.Abstractions.Authentication;
using Application.Abstractions.Authentication.SMS;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Otps.Generate;

internal sealed class GenerateOtpCommandHandler(
    IApplicationDbContext context,
    IOtpService otp)
    : ICommandHandler<GenerateOtpCommand>
{
    public async Task<Result> Handle(GenerateOtpCommand command, CancellationToken cancellationToken)
    {
        User? user = await context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.MobileNumber == command.MobileNumber, cancellationToken);

        if (user is null)
        {
            return Result.Failure<string>(UserErrors.NotFoundByMobileNumber);
        }

        return await otp.Send(user.MobileNumber, cancellationToken);
    }
}
