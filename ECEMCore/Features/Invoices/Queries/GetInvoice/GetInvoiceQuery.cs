
using ECEMCore.Abstraction.Caching;
using ECEMCore.Abstraction.Messaging.Queries;

namespace ECEMCore.Features.Invoices.Queries.GetInvoice;


//**Before Using Cache Distributes
//public record GetInvoiceQuery(Guid InvoiceId) : IQuery<InvoiceResponse>;
//**After Using Cache Distributes
public record GetInvoiceQuery(Guid InvoiceId) : IQuery<InvoiceResponse>, ICachedQuery
{
    public string CacheKey => $"Invoice-{InvoiceId}";

    public TimeSpan? Expiration => null;
}
