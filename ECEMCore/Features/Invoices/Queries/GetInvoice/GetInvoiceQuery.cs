
using ECEMCore.Abstraction.Messaging.Queries;

namespace ECEMCore.Features.Invoices.Queries.GetInvoice;

public record GetInvoiceQuery(Guid InvoiceId) : IQuery<InvoiceResponse>;

//public record GetInvoiceQuery(Guid InvoiceId) : IQuery<InvoiceResponse>, ICachedQuery
//{
//    public string CacheKey => $"Invoice-{InvoiceId}";

//    public TimeSpan? Expiration => null;
//}
