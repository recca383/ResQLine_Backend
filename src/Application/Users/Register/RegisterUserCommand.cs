using Application.Abstractions.Messaging;

namespace Application.Users.Register;

public sealed record RegisterUserCommand(
    string MobileNumber,
    string FirstName,
    string LastName)
    : ICommand<Guid>;
