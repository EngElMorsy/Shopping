


using ECEMInfrastructure.Outbox;
using ECMDomain.Abstraction;
using ECMDomain.Attributes;
using ECMDomain.Entities.Employees;
using ECMDomain.Entities.Identity.Roles;
using ECMDomain.Entities.Identity.Users;
using ECMDomain.Entities.Invoicces;
using ECMDomain.Entities.InvoiceItems;
using ECMDomain.Entities.Products;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Reflection;

namespace ECEMInfrastructure
{
    public class AppDbContext :IdentityDbContext<AppUser, AppRole, Guid>
    {
        //**  Berfore USe IPublisher For Update Emp Balance When AAdd Invoiceor...
        //public AppDbContext(DbContextOptions<AppDbContext> options):base(options) 
        //{ 

        //} 

        //**AfterUSe Update Event Publish 
        private static readonly JsonSerializerSettings JsonSerializerSettings = new()
        {
            TypeNameHandling = TypeNameHandling.All,
        };
        //**Before Update Event Publish
        // private readonly IPublisher _publisher;
        //public AppDbContext(DbContextOptions<AppDbContext> options, IPublisher publisher) : base(options)
        //{
        //    _publisher = publisher;
        //}
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Employee> Employees { get; set; } = null!; 
        public DbSet<Product> Products { get; set; } = null!; 
        public DbSet<Invoice> Invoices { get; set; } = null!; 
        public DbSet<InvoiceItem> InvoiceItems { get; set; } = null!;
        public DbSet<OutboxMessage> OutboxMessages { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            ProcessAutoseedData(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddDomainEventsAsOutboxMessages();
            var result = await base.SaveChangesAsync(cancellationToken);
           //** Before Use Update Publish Event 
            //await PublishDomainEvents();
            return result;
        }
        //** Before Use Update Publish Event
        //private async Task PublishDomainEvents()
        //{
        //    var domainEvents = ChangeTracker
        //        .Entries<BaseEntity>()
        //        .Select(e => e.Entity)
        //        .SelectMany(entity =>
        //        {
        //            var domainEvents = entity.GetDomainEvents();
        //            entity.ClearDomainEvents();
        //            return domainEvents;
        //        }).ToList();
        //    foreach (var domainEvent in domainEvents)
        //    {
        //        await _publisher.Publish(domainEvent);
        //    }
        //} 

        private void AddDomainEventsAsOutboxMessages()
        {
            var outboxMessages = ChangeTracker
                //**Before Auto Seed
                //.Entries<BaseEntity>()
                .Entries<IDomainEventRaiser>()
                .Select(entry => entry.Entity)
                .SelectMany(entity =>
                {
                    var domainEvents = entity.GetDomainEvents();

                    entity.ClearDomainEvents();

                    return domainEvents;
                })
                .Select(domainEvent => new OutboxMessage(
                    Guid.NewGuid(),
                    DateTime.UtcNow,
                    domainEvent.GetType().Name,
                    JsonConvert.SerializeObject(domainEvent, JsonSerializerSettings)))
                .ToList();

            AddRange(outboxMessages);
        }

        private void ProcessAutoseedData(ModelBuilder modelBuilder)
        {
            var entityTypes = modelBuilder.Model.GetEntityTypes()
                .Select(x => x.ClrType)
                .Where(x => x.GetInterface(nameof(IHaveAutoseedData)) != null)
                .SelectMany(e => e.GetProperties())
                .Where(e => e.GetCustomAttribute<AutoSeedDataAttribute>() != null)
                .GroupBy(e => e.DeclaringType)
                .ToList();

            foreach (var group in entityTypes)
            {
                var entityType = modelBuilder.Entity(group.Key!);

                foreach (var property in group)
                {
                    var value = property.GetValue(Activator.CreateInstance(property.DeclaringType!));

                    entityType.HasData(value ?? throw new InternalServerException(
                        "AutoseedFailure.Error",
                        ["PropertyInfo value null error"]));
                }
            }
        }

    }
}
