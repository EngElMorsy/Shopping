
namespace ECMDomain.Abstraction
{
    public abstract class BaseEntity
    {
        private readonly List<IDomainEvents> _domainevent = [];
        protected BaseEntity()
        {
            
        }
        protected BaseEntity(Guid id)
            => Id = id;

        public Guid Id { get; init; }
        public byte[] RowVersion { get; set; } = null!;

        public IReadOnlyList<IDomainEvents> GetDomainEvents()
        => _domainevent.ToList(); 
        public void ClearDomainEvents()=> _domainevent.Clear();
        protected void RaiseDomainEvent(IDomainEvents domainEvents)
           => _domainevent.Add(domainEvents);
    }
}
