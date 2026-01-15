
using ECMDomain.Abstraction;
using MediatR;


namespace ECEMCore.Abstraction.Messaging.Commands;
 

//** Before Use IPipeLineBehavoir
//public interface ICommand:IRequest<Result<NoContentDto>>, IBaseCommand;

//public interface ICommand<TResponse> :IRequest<Result<TResponse>>, IBaseCommand
//    where TResponse:IResult; 
//public interface IBaseCommand; 

//** After Use IPipeLineBehavouir
public interface ICommand:IRequest<Result<NoContentDto>>;

public interface ICommand<TResponse> :IRequest<Result<TResponse>>
    where TResponse:IResult; 



