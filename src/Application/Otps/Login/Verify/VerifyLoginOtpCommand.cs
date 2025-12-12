using Application.Abstractions.Messaging;

namespace Application.Otps.Login.Verify;
public sealed record VerifyLoginOtpCommand(string MobileNumber, string Otp) : ICommand<string>;
