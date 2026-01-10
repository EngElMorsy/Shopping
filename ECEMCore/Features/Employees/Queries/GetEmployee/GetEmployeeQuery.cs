

using ECEMCore.Abstraction.Messaging.Queries;

namespace ECEMCore.Features.Employees.Queries.GetEmployee
{
    public record GetEmployeeQuery(Guid employeeId) : IQuery<EmployeeResponse>;
 

    //public record GetEmployeeQuery(Guid employeeId) : IQuery<EmployeeResponse>, ICachedQuery
    //{
    //    public string CacheKey => $"employee-{employeeId}";

    //    public TimeSpan? Expiration => null;
    //}
}
