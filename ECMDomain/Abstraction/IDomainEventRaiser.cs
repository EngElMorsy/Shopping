namespace ECMDomain.Abstraction;

public interface IDomainEventRaiser
{
    IReadOnlyList<IDomainEvents> GetDomainEvents();
    void ClearDomainEvents();
    void RaiseDomainEvent(IDomainEvents domainEvent);
}