using SharedKernel;

namespace Domain.Users.Events;

public sealed record UseUpdateDetailsDomainEvent(Guid UserId) : IDomainEvent;
