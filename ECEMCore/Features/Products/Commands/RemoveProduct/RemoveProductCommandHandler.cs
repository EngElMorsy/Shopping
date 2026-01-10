using ECEMCore.Abstraction.Messaging.Commands;
using ECMDomain.Abstraction;
using ECMDomain.Entities.Products;

namespace ECEMCore.Features.Products.Commands.RemoveProduct
{
    public class RemoveProductCommandHandler(IUnitWork unitWork)
        : ICommandHandler<RemoveProductCommand>
    {
        private readonly IUnitWork _unitWork = unitWork;

        public  async Task<Result<NoContentDto>> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {

            var product = await _unitWork.Repostiry<Product>()
                .GetIdAsync(request.ProductId);

            if (product is null)
                return Result<NoContentDto>
                    .Failed(400, "Null.Error", $"The Product with The Id :{request.ProductId}");
           
                 _unitWork.Repostiry<Product>().Delete(product); 
                 await _unitWork.CommitAsync(cancellationToken);
                return Result<NoContentDto>
                .Success(204);
        }
    }
}
