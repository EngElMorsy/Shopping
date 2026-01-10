

using ECEMCore.Abstraction.Messaging.Commands;
using ECMDomain.Entities.Products.DTOs;

namespace ECEMCore.Features.Products.Commands.UpdateProduct
{
    public record UpdateProductCommand(Guid ProductId, UpdateProductDto Dto) : ICommand;
   
}
