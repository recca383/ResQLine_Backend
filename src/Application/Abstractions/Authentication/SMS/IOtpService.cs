using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel;

namespace Application.Abstractions.Authentication.SMS;
public interface IOtpService
{
    Task<Result> Send(string mobileNumber, CancellationToken cancellationtoken);
    Task<Result> Verify(string mobileNumber, string otp, ITokenProvider tokenProvider, CancellationToken cancellationToken);
}
