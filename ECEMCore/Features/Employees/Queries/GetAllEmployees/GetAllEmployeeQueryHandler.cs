

using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECEMCore.Abstraction.Messaging.Queries;
using ECMDomain.Abstraction;
using ECMDomain.Entities.Employees;
using Microsoft.EntityFrameworkCore;

namespace ECEMCore.Features.Employees.Queries.GetAllEmployees
{
    internal sealed class GetAllEmployeeQueryHandler(IUnitWork unitWork, IMapper mapper) :
        IQueryHandler<GetAllEmployeeQuery, EmployeeResponseCollection>
    {
        private readonly IUnitWork _unitWork = unitWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<EmployeeResponseCollection>> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
        {
            var employee = await _unitWork.Repostiry<Employee>()
                .GetAll()
                .ProjectTo<EmployeeResponse>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            var response = new EmployeeResponseCollection
            {
                Employees = employee.AsReadOnly()
            }; 

            //return Result<EmployeeResponseCollection>.Failed(200,"Test Error","Initial Test");
            return Result<EmployeeResponseCollection>.Success(response, 200);
        }
    }
}
