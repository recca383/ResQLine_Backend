using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Authentication;
using Application.Abstractions.Authentication.SMS;
using Application.Abstractions.Data;
using Domain.OtpStores;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SharedKernel;

namespace Infrastructure.Authentication.SMS;
internal sealed class OtpService(
    IConfiguration configuration,
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider,
    ITokenProvider tokenProvider,
    ISmsSender sender) : IOtpService
{
    public async Task<Result> Send(string mobileNumber, CancellationToken cancellationtoken)
    {
        int otpLength = configuration.GetValue<int>("Otp:Length");
        int expiryinminutes = configuration.GetValue<int>("Otp:ExpiryInMinutes");

        byte[] bytes = new byte[otpLength];
        RandomNumberGenerator.Fill(bytes);

        var result = new StringBuilder(otpLength);

        foreach(byte b in bytes)
        {
            result.Append((b % 10).ToString(CultureInfo.InvariantCulture));
        }
        
        var otp = new OtpStore()
        {
            Id = Guid.NewGuid(),
            MobileNumber = mobileNumber,
            Otp = result.ToString(),
            Expiry = dateTimeProvider.UtcNow.AddMinutes(expiryinminutes)

        };

        string message = configuration.GetValue<string>("Otp:Message") + otp.Otp;

        if(sender.SendMessage(mobileNumber, message) == null)
        {
            return Result.Failure<bool>(OtpErrors.OtpFailedToSend);
        }

        context.OtpStores.Add(otp);
        await context.SaveChangesAsync(cancellationtoken);

        return Result.Success(true);
    }

    public async Task<Result<string>> Verify(
        OtpStore storedOtp,
        string otp,
        CancellationToken cancellationToken)
    {
        if (storedOtp.Otp != otp)
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
            .SingleOrDefaultAsync(s => s.MobileNumber == storedOtp.MobileNumber, cancellationToken);

        string token = tokenProvider.Create(user!);

        return Result<string>.Success(token);
            
    }
}
