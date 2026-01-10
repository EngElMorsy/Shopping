

using ECEMCore.Abstraction.Messaging.Commands;
using ECMDomain.Entities.Employees.DTOs;

namespace ECEMCore.Features.Employees.Commands.UpdateEmployee
{
    public record UpdateEmployeeCommand(Guid EmployeeId,UpdateEmployeeDto Dto) :ICommand;
    
}
