

using AutoMapper;
using ECEMCore.Abstraction.Messaging.Queries;
using ECMDomain.Abstraction;
using ECMDomain.Entities.Products;

namespace ECEMCore.Features.Products.Queries.GetProduct
{
    internal sealed class GetProductQueryHandler(IUnitWork unitWork,IMapper mapper)
        : IQueryHandler<GetProductQuery, ProductResponse>
    {
        private readonly IUnitWork _unitWork = unitWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<ProductResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _unitWork.Repostiry<Product>()
               .GetIdAsync(request.ProductId);

            if (product is null)
                return Result<ProductResponse>
                    .Failed(400, "Null.Error", $"The Product with The Id :{request.ProductId}");
          
            var reponse = _mapper.Map<ProductResponse>(product);
            return Result<ProductResponse>.Success(reponse,200);
        }
    }
}
