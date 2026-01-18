

using ECMDomain.Abstraction;

namespace ECMDomain.Entities.Identity.Users.Events;
public record UserRegisteredDomainEvent(
    Guid UserId,
    string? AdminKey) : IDomainEvents;
