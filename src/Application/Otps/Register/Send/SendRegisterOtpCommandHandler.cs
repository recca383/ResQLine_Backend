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

namespace Application.Otps.Register.Send;
internal sealed class SendRegisterOtpCommandHandler(
    IApplicationDbContext context,
    IOtpService otp,
    ISmsSender smsSender) :
    ICommandHandler<SendRegisterOtpCommand>
{
    public async Task<Result> Handle(SendRegisterOtpCommand command, CancellationToken cancellationToken)
    {
        bool isUserExists = await context.Users
            .AsNoTracking()
            .AnyAsync(u =>
            u.MobileNumber == command.MobileNumber,
            cancellationToken);

        List<OtpStore> storedOtps = await context.OtpStores
            .Where(o =>
                o.MobileNumber == command.MobileNumber &&
                o.OtpType == OtpType.Register)
            .ToListAsync(cancellationToken);

        context.OtpStores.RemoveRange(storedOtps);

        if (isUserExists)
        {
            return Result.Failure<string>(UserErrors.MobileNumberNotUnique);
        }

        OtpStore createdOtp = otp.Generate(command.MobileNumber);

        bool IsSuccess = await smsSender.SendMessage(
            command.MobileNumber,
            otp.GetOtpMessage(createdOtp.Otp));

        if (!IsSuccess)
        {
            return Result.Failure(OtpErrors.OtpFailedToSend);
        }

        createdOtp.OtpType = OtpType.Register;

        context.OtpStores.Add(createdOtp);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
