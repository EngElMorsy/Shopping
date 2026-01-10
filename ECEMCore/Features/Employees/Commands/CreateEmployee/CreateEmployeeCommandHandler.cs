

using AutoMapper;
using ECEMCore.Abstraction.Messaging.Commands;
using ECMDomain.Abstraction;
using ECMDomain.Entities.Employees;

namespace ECEMCore.Features.Employees.Commands.CreateEmployee
{
    internal sealed class CreateEmployeeCommandHandler(
        IUnitWork unitOfwork, IMapper mapper) : ICommandHandler<CreateEmployeeCommand, EmployeeResponse>
    {
        private readonly IUnitWork _unitOfwork = unitOfwork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<EmployeeResponse>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employees = Employee.Create(request.Dto);
            object value = await _unitOfwork.Repostiry<Employee>().CreateAsync(employees, cancellationToken);
            await _unitOfwork.CommitAsync(cancellationToken);
            var response = _mapper.Map<EmployeeResponse>(employees); 

            return Result<EmployeeResponse>
                .Success(response, 201);

        }
    }
}
