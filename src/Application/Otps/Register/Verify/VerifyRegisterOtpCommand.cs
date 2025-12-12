using Application.Abstractions.Messaging;

namespace Application.Otps.Register.Verify;
public sealed record VerifyRegisterOtpCommand(
    string MobileNumber,
    string FirstName,
    string LastName,
    string Otp) : ICommand;
