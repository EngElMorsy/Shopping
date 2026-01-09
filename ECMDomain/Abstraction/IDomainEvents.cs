
using MediatR;
namespace ECMDomain.Abstraction
{

    ////This Before Use Mediator Contract
    // public interface IDomainEvents; 
    
    ////This After Use Mediator Contract 
    ///for Send Message for More Client
    public interface IDomainEvents:INotification;

}
