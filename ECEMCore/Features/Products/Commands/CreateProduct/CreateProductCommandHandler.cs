

using AutoMapper;
using ECEMCore.Abstraction.Messaging.Commands;
using ECMDomain.Abstraction;
using ECMDomain.Entities.Products;

namespace ECEMCore.Features.Products.Commands.CreateProduct
{

    internal sealed class CreateProductCommandHandler(IUnitWork unitOfwork, IMapper mapper)
        : ICommandHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IUnitWork _unitOfwork = unitOfwork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<ProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = Product.Create(request.Dto);
            object value = await _unitOfwork.Repostiry<Product>().CreateAsync(product, cancellationToken);
            await _unitOfwork.CommitAsync(cancellationToken);
            var response = _mapper.Map<ProductResponse>(product);
            return Result<ProductResponse>.Success(response, 201);
        }
    }
}
