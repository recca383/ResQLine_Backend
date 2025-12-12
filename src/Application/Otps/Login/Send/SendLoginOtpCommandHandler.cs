using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Authentication.SMS;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.OtpStores;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Otps.Login.Send;
internal sealed class SendLoginOtpCommandHandler(
    IApplicationDbContext context,
    IOtpService otp,
    ISmsSender smsSender) :
    ICommandHandler<SendLoginOtpCommand>
{
    public async Task<Result> Handle(SendLoginOtpCommand command, CancellationToken cancellationToken)
    {
        User? user = await context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u =>
            u.MobileNumber == command.MobileNumber,
            cancellationToken);

        if(user is null)
        {
            return Result.Failure<string>(UserErrors.NotFoundByMobileNumber);
        }

        OtpStore createdOtp = otp.Generate(user.MobileNumber);

        bool IsSuccess = await smsSender.SendMessage(
            user.MobileNumber,
            otp.GetOtpMessage(createdOtp.Otp));

        if (!IsSuccess)
        {
            return Result.Failure(OtpErrors.OtpFailedToSend);
        }

        createdOtp.OtpType = OtpType.Login;

        context.OtpStores.Add(createdOtp);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
