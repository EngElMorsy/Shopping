
using ECMDomain.Abstraction;
using MediatR;

namespace ECEMCore.Abstraction.Messaging.Queries;

//** this interface inherite from IRequest from mediator 

//** in case Read Two cases 
//** one Case IF i have read data without Give Request Data 
//** two case if i want read Data With Give Request Data 
//** For this Cases Must IQuery Generic  
//**IQuery generic Take Tresponse While TResponse Is Interface Or Abstract Class 

public interface IQuery<TResponse>:IRequest<Result<TResponse>> 
                       where TResponse :IResult;
