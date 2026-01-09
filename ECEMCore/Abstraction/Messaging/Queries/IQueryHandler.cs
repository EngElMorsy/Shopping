using ECMDomain.Abstraction;
using MediatR;
namespace ECEMCore.Abstraction.Messaging.Queries;
 
//** THis Handler For IRequest From type Iquery  
//** so IQueryHandler For Type IRequest
    public interface IQueryHandler <TQuery,TResponse>: IRequestHandler<TQuery,Result<TResponse>> 
        where TQuery:IQuery<TResponse> 
        where TResponse:IResult;
    

