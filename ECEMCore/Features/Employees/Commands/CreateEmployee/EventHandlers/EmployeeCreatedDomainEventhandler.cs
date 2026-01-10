

using ECEMCore.Abstraction.Emailing;
using ECMDomain.Abstraction;
using ECMDomain.Entities.Employees;
using ECMDomain.Entities.Employees.Events;
using MediatR;

namespace ECEMCore.Features.Employees.Commands.CreateEmployee.EventHandlers
{

    internal sealed class EmployeeCreatedDomainEventhandler(IUnitWork unitWork,
        IEmailService emailService) :
        INotificationHandler<EmployeeCreateDomainEvent>
    {
        private readonly IUnitWork _unitWork = unitWork;
        private readonly IEmailService _emailService = emailService;

        public async Task Handle(EmployeeCreateDomainEvent notification, CancellationToken cancellationToken)
        {
            var employee = await _unitWork.Repostiry<Employee>()
             .GetIdAsync(notification.employeeId, cancellationToken);

            if (employee is null)
                return;  //Search 

            await _emailService.sendAsync();
        }
    }
}
