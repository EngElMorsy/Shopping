

using ECEMCore.Abstraction.Caching;
using ECEMCore.Abstraction.Messaging.Queries;

namespace ECEMCore.Features.Employees.Queries.GetEmployee
{ 
    //**Before Using Cache Distributes
    //public record GetEmployeeQuery(Guid employeeId) : IQuery<EmployeeResponse>;

    //**After Using Cache Distributes
    public record GetEmployeeQuery(Guid employeeId) : IQuery<EmployeeResponse>, ICachedQuery
    {
        public string CacheKey => $"employee-{employeeId}";

        public TimeSpan? Expiration => null;
    }
}
