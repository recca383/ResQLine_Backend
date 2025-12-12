using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.OtpStores;
using Domain.Users;
using SharedKernel;

namespace Application.Otps.Register.Verify;
internal sealed class VerifyRegisterOtpCommandHandler(
    IApplicationDbContext context
    ) :
    ICommandHandler<VerifyRegisterOtpCommand>
{
    public async Task<Result> Handle(VerifyRegisterOtpCommand command, CancellationToken cancellationToken)
    {
        OtpStore storedOtp = context.OtpStores
            .FirstOrDefault(otp => otp.MobileNumber == command.MobileNumber
                                && otp.OtpType == OtpType.Register);

        if(storedOtp is null)
        {
            return Result.Failure(OtpErrors.NoOtpRequested(command.MobileNumber));
        }

        if(storedOtp.Expiry < DateTime.UtcNow)
        {
            return Result.Failure(OtpErrors.OtpExpired);
        }

        if(storedOtp.Otp != command.Otp)
        {
            return Result.Failure(OtpErrors.OtpNotEqual);
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            MobileNumber = command.MobileNumber,
            FirstName = command.FirstName,
            LastName = command.LastName,
        };

        context.Users.Add(user);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();

    }
}

