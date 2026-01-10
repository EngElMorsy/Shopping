

using ECEMCore.Abstraction.Messaging.Commands;
using ECMDomain.Abstraction;
using ECMDomain.Entities.Employees;
using Microsoft.EntityFrameworkCore;

namespace ECEMCore.Features.Employees.Commands.RemoveEmployee
{
    internal sealed class RemoveEmployeeCommandHandler(
        IUnitWork unitWork) : ICommandHandler<RemoveEmployeeCommand>
    {
        private readonly IUnitWork _unitWork = unitWork;

        public async Task<Result<NoContentDto>> Handle(RemoveEmployeeCommand request, CancellationToken cancellationToken)
        {
            var Employees = await _unitWork.Repostiry<Employee>()
                .GetAll().Include(x => x.Invoice).FirstOrDefaultAsync(x => x.Id == request.EmployeeId);
             //.GetIdAsync(request.employeeId);

            if (Employees is null)
                return Result<NoContentDto>
                    .Failed(400, "Null.Error", $"The Employee with The Id :{request.EmployeeId}");

            if (Employees.Invoice.Count>0)
                return Result<NoContentDto>
                    .Failed(400, "Invalid Error", $"The Employee with The Id :{request.EmployeeId}has invoices");



            _unitWork.Repostiry<Employee>().Delete(Employees);
            await _unitWork.CommitAsync(cancellationToken);
            return Result<NoContentDto>
                .Success(204);
        }
    }
}
