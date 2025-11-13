using SharedKernel;

namespace Domain.Users.Events;

public sealed record UserRegisteredDomainEvent(Guid UserId) : IDomainEvent;
