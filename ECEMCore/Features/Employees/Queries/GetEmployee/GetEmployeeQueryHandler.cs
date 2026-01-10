

using AutoMapper;
using ECEMCore.Abstraction.Messaging.Queries;
using ECMDomain.Abstraction;
using ECMDomain.Entities.Employees;

namespace ECEMCore.Features.Employees.Queries.GetEmployee
{
    public sealed class GetEmployeeQueryHandler(IUnitWork unitWork, IMapper mapper)
        : IQueryHandler<GetEmployeeQuery, EmployeeResponse>
    {
        private readonly IUnitWork _unitWork = unitWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<EmployeeResponse>> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            var employee = await _unitWork.Repostiry<Employee>()
              .GetIdAsync(request.employeeId);

            if (employee is null)
                return Result<EmployeeResponse>
                    .Failed(400, "Null.Error", $"The Product with The Id :{request.employeeId}");

            var reponse = _mapper.Map<EmployeeResponse>(employee);
            return Result<EmployeeResponse>.Success(reponse, 200);
        }
    }
}
