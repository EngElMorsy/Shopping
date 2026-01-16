

using ECEMCore.Abstraction.Caching;
using ECEMCore.Abstraction.Messaging.Queries;

namespace ECEMCore.Features.Products.Queries.GetProduct
{
    //**Before Using Cache Distributes
    // public record GetProductQuery(Guid ProductId) : IQuery<ProductResponse>;
    //**After Using Cache Distributes
    public record GetProductQuery(Guid ProductId) : IQuery<ProductResponse>, ICachedQuery
    {
        public string CacheKey => $"Product-{ProductId}";

        public TimeSpan? Expiration => null;
    }
}
