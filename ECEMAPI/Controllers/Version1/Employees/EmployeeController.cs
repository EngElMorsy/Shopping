using Asp.Versioning;
using ECEMCore.Features.Employees.Commands.CreateEmployee;
using ECEMCore.Features.Employees.Commands.RemoveEmployee;
using ECEMCore.Features.Employees.Commands.UpdateEmployee;
using ECEMCore.Features.Employees.Queries.GetAllEmployees;
using ECEMCore.Features.Employees.Queries.GetEmployee;
using ECMDomain.Entities.Employees.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECEMAPI.Controllers.Version1.Employees
{
   // [Route("api/[controller]")] 
    [ApiVersion(ApiVersions.V1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class EmployeeController(ISender sender)
        : BaseController
    {
        private readonly ISender _sender = sender;
       
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeDto request
            ,CancellationToken cancellation=default)
        {
            var response = await _sender.Send(new CreateEmployeeCommand(request),cancellation);
             return CreateResult(response);
        }
      
        [HttpGet("{EmployeeId}")]
        public async Task<IActionResult> GetEmployeeAsync(Guid EmployeeId, CancellationToken cancellation = default)
        {
            var response = await _sender.Send(
                new GetEmployeeQuery(EmployeeId), cancellation); 
            return CreateResult(response);
        }
       
        [HttpGet]
        public async Task<IActionResult> GetAllEmployeeAsync(CancellationToken cancellation = default)
        {
            var response = await _sender.Send(
                new GetAllEmployeeQuery(), cancellation);
            return CreateResult(response);
        }

        [HttpPut("{EmployeeId}")]
        public async Task<IActionResult>UpdateEmployeeAsync(
            Guid EmployeeId,
            UpdateEmployeeDto request,
            CancellationToken cancellation = default)
        {
            var response = await _sender.Send(
                new UpdateEmployeeCommand(EmployeeId,request), cancellation);
            return CreateResult(response);
        }
      
        [HttpDelete("{EmployeeId}")]
        public async Task<IActionResult> DeleteEmployeeAsync(Guid EmployeeId,
           CancellationToken cancellation = default)
        {
            var response = await _sender.Send(
                new RemoveEmployeeCommand (EmployeeId), cancellation);
            return CreateResult(response);
        }

    }
}
