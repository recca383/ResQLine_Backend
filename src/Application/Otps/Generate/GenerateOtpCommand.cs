using Application.Abstractions.Messaging;

namespace Application.Otps.Generate;

public sealed record GenerateOtpCommand(string MobileNumber) : ICommand;
