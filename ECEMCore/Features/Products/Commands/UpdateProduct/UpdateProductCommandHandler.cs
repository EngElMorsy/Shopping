

using ECEMCore.Abstraction.Messaging.Commands;
using ECMDomain.Abstraction;
using ECMDomain.Entities.Products;

namespace ECEMCore.Features.Products.Commands.UpdateProduct
{
    public sealed class UpdateProductCommandHandler(IUnitWork unitWork)
        : ICommandHandler<UpdateProductCommand>
    {
        private readonly IUnitWork _unitWork = unitWork;

        public async Task<Result<NoContentDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitWork.Repostiry<Product>()
           .GetIdAsync(request.ProductId);
            if (product is null)
                return Result<NoContentDto>
                    .Failed(400, "Null.Error", $"The Product with The Id :{request.ProductId}");

               product.Update(request.Dto);
               _unitWork.Repostiry<Product>()
                .Update(product); 
            await _unitWork.CommitAsync(cancellationToken,chekForConcurrency: true);

            return Result<NoContentDto>
                .Success(204);

        }
    }
}
