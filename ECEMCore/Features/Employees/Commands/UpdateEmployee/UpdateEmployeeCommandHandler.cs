
using ECEMCore.Abstraction.Messaging.Commands;
using ECMDomain.Abstraction;
using ECMDomain.Entities.Employees;

namespace ECEMCore.Features.Employees.Commands.UpdateEmployee
{
    internal sealed class UpdateEmployeeCommandHandler(IUnitWork unitWork)
        : ICommandHandler<UpdateEmployeeCommand>
    {
        private readonly IUnitWork _unitWork = unitWork;

        public async Task<Result<NoContentDto>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {

            var employee = await _unitWork.Repostiry<Employee>()
            .GetIdAsync(request.EmployeeId,cancellationToken); 

            if (employee is null)
                return Result<NoContentDto>                               //** request.Dto.Id this if Send ID In DTO
                    .Failed(400, "Null.Error", $"The Employee with The Id :{request.EmployeeId}");

            employee.update(request.Dto);
           
            _unitWork.Repostiry<Employee>()
             .Update(employee);
            //**BeFore Cutom Expection Type 
            //**For ConCurencyException The Operation Must be Set chekForConcurrency true 
            //await _unitWork.CommitAsync(cancellationToken,chekForConcurrency);
             await _unitWork.CommitAsync(cancellationToken,chekForConcurrency:true);

            return Result<NoContentDto>
                .Success(204);
        }
    }
}
