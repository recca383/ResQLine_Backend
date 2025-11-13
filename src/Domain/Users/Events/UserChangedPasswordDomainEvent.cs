using SharedKernel;

namespace Domain.Users.Events;

public sealed record UserChangedPasswordDomainEvent(Guid UserId) : IDomainEvent;
