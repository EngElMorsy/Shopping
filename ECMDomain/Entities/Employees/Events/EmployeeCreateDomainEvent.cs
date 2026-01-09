
using ECMDomain.Abstraction;

namespace ECMDomain.Entities.Employees.Events
{
   public record EmployeeCreateDomainEvent(Guid employeeId):IDomainEvents;
   
}
