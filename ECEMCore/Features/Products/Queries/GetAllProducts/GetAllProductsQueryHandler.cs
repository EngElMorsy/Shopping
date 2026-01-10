

using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECEMCore.Abstraction.Messaging.Queries;
using ECMDomain.Abstraction;
using ECMDomain.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace ECEMCore.Features.Products.Queries.GetAllProducts
{
    internal sealed class GetAllProductsQueryHandler(IUnitWork unitWork, IMapper mapper) : 
        IQueryHandler<GetAllProductsQuery,ProductResponseCollection>
    {
        private readonly IUnitWork _unitWork = unitWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<ProductResponseCollection>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _unitWork.Repostiry<Product>()
                .GetAll()
                .ProjectTo<ProductResponse>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            var response = new ProductResponseCollection
            { 
                Products=products.AsReadOnly()
            };
             return Result<ProductResponseCollection>.Success(response, 200);
            //return Result<ProductResponseCollection>.Failed(400, new Error
            //{
            //    ErrorCode = "Test Error" ,
            //    ErrorMessages=["Test one","Test two","Test Three"]
                

            //});
        }
    }
}
