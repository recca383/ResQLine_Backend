using Application.Abstractions.Messaging;

namespace Application.Otps.Login.Send;

public sealed record SendLoginOtpCommand(string MobileNumber) : ICommand;
