using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Authentication.SMS;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.OtpStores;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Otps.Verify;
internal sealed class VerifyOtpComamndHandler(
    IApplicationDbContext context,
    IOtpService otpservice)
    : ICommandHandler<VerifyOtpCommand, string>
{
    public async Task<Result<string>> Handle(VerifyOtpCommand command, CancellationToken cancellationToken)
    {
        OtpStore storedOtp = await context.OtpStores
            .AsNoTracking()
            .OrderByDescending(x => x.Expiry)
            .FirstOrDefaultAsync(u => u.MobileNumber == command.mobileNumber,
            cancellationToken);

        if(storedOtp == null)
        {
            return Result.Failure<string>(OtpErrors.NoOtpRequested(command.mobileNumber));
        }

        return await otpservice.Verify(storedOtp, command.otp, cancellationToken);
    }
}
