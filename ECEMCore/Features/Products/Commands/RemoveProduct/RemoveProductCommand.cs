
using ECEMCore.Abstraction.Messaging.Commands;
namespace ECEMCore.Features.Products.Commands.RemoveProduct
{
    public record RemoveProductCommand(Guid ProductId):ICommand;
  
}
