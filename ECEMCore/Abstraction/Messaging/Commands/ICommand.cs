
using ECMDomain.Abstraction;
using MediatR;


namespace ECEMCore.Abstraction.Messaging.Commands;

public interface ICommand:IRequest<Result<NoContentDto>>, IBaseCommand;

public interface ICommand<TResponse> :IRequest<Result<TResponse>>, IBaseCommand
    where TResponse:IResult; 


public interface IBaseCommand;
