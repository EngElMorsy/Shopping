

using ECEMCore.Abstraction.Messaging.Queries;

namespace ECEMCore.Features.Products.Queries.GetProduct
{
    public record GetProductQuery(Guid ProductId) : IQuery<ProductResponse>;
  
    //public record GetProductQuery(Guid ProductId) : IQuery<ProductResponse>, ICachedQuery
    //{
    //    public string CacheKey => $"Product-{ProductId}";

    //    public TimeSpan? Expiration => null;
    //}
}
