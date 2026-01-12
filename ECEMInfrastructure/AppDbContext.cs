


using ECMDomain.Abstraction;
using ECMDomain.Entities.Employees;
using ECMDomain.Entities.Invoicces;
using ECMDomain.Entities.InvoiceItems;
using ECMDomain.Entities.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECEMInfrastructure
{
    public class AppDbContext :DbContext
    {
        //**  Berfore USe IPublisher For Update Emp Balance When AAdd Invoiceor...
        //public AppDbContext(DbContextOptions<AppDbContext> options):base(options) 
        //{ 

        //}
        private readonly IPublisher _publisher;
        public AppDbContext(DbContextOptions<AppDbContext> options, IPublisher publisher) : base(options)
        {
            _publisher = publisher;
        }

        public DbSet<Employee> Employees { get; set; } = null!; 
        public DbSet<Product> Products { get; set; } = null!; 
        public DbSet<Invoice> Invoices { get; set; } = null!; 
        public DbSet<InvoiceItem> InvoiceItems { get; set; } = null!; 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            //ProcessAutoseedData(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            await PublishDomainEvents();
            return result;
        }
        private async Task PublishDomainEvents()
        {
            var domainEvents = ChangeTracker
                .Entries<BaseEntity>()
                .Select(e => e.Entity)
                .SelectMany(entity =>
                {
                    var domainEvents = entity.GetDomainEvents();
                    entity.ClearDomainEvents();
                    return domainEvents;
                }).ToList();
            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent);
            }
        }

    }
}
