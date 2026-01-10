

using ECEMCore.Abstraction.Messaging.Commands;
using ECMDomain.Entities.Employees.DTOs;

namespace ECEMCore.Features.Employees.Commands.CreateEmployee
{
    public record CreateEmployeeCommand(CreateEmployeeDto Dto): 
        ICommand<EmployeeResponse>;
 
}
