using Application.Abstractions.Messaging;

namespace Application.Otps.GenerateOtp;

public sealed record GenerateOtpCommand(string MobileNumber) : ICommand;
