


using ECEMCore.Abstraction.Messaging.Commands;
using ECMDomain.Entities.Products.DTOs;

namespace ECEMCore.Features.Products.Commands.CreateProduct;

public record CreateProductCommand(
     CreateProductDto Dto):ICommand<ProductResponse>;
