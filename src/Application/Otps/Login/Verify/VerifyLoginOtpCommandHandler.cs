using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.OtpStores;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using static System.Net.WebRequestMethods;

namespace Application.Otps.Login.Verify;
internal sealed class VerifyLoginOtpCommandHandler(
    IApplicationDbContext context,
    ITokenProvider tokenProvider) :
    ICommandHandler<VerifyLoginOtpCommand>
{
    public async Task<Result> Handle(VerifyLoginOtpCommand command, CancellationToken cancellationToken)
    {
        OtpStore? storedOtp = await context.OtpStores
            .AsNoTracking()
            .Where(s => s.OtpType == OtpType.Login)
            .SingleOrDefaultAsync(s => s.MobileNumber == command.MobileNumber,
             cancellationToken);

        if (storedOtp is null)
        {
            return Result.Failure<string>(OtpErrors.NoOtpRequested(command.MobileNumber));
        }

        if (storedOtp!.Otp != command.Otp)
        {
            return Result.Failure<string>(OtpErrors.OtpNotEqual);
        }

        if (storedOtp.Expiry < DateTime.UtcNow)
        {
            return Result.Failure<string>(OtpErrors.OtpExpired);
        }

        context.OtpStores.Remove(storedOtp);
        await context.SaveChangesAsync(cancellationToken);

        User user = await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.MobileNumber == storedOtp.MobileNumber, cancellationToken);

        string token = tokenProvider.Create(user!);

        return Result<string>.Success(token);
    }
}
