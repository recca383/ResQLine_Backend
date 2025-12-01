using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel;

namespace Domain.OtpStores;
public static class OtpErrors
{
    public static readonly Error OtpFailedToSend = Error.Conflict(
    "Otp.SendingFailed",
    "Otp Failed to send");

    public static Error NoOtpRequested(string mobileNumber) => Error.NotFound(
    "Otp.NotFound",
    $"{mobileNumber} did not request Otp");

    public static readonly Error OtpNotEqual = Error.Failure(
    "Otp.NotEqual",
    "Otp Not Equal");

    public static readonly Error OtpExpired = Error.Failure(
    "Otp.Expired",
    "Failed to verify OTP on time");
}
