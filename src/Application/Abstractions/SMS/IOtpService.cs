using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.OtpStores;
using SharedKernel;

namespace Application.Abstractions.Authentication.SMS;
public interface IOtpService
{
    string GetOtpMessage(string otp);
    OtpStore Generate(string mobileNumber);
    Task<Result<string>> Verify(OtpStore storedOtp, string otp, CancellationToken cancellationToken);
}
