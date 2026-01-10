using ECMDomain.Abstraction;
namespace ECMDomain.Entities.Invoicces;

public record InvoiceCreatedDomainEvent(Guid invoiceId):IDomainEvents;

