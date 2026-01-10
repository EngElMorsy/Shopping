

using ECEMCore.Abstraction.Messaging.Commands;

namespace ECEMCore.Features.Employees.Commands.RemoveEmployee;

public record RemoveEmployeeCommand(Guid EmployeeId) :ICommand;

