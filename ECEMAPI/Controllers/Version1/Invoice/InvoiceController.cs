using AECEMCore.Features.Invoices.Commands.UpdateInvoice;
using Asp.Versioning;
using ECEMAPI.Controllers.Version1.Employees;
using ECEMCore.Features.Invoices.Commands.CreateInvoice;
using ECEMCore.Features.Invoices.Commands.RemoveInvoice;
using ECEMCore.Features.Invoices.Queries.GetAllInvoice;
using ECEMCore.Features.Invoices.Queries.GetInvoice;
using ECMDomain.Entities.Invoicces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECEMAPI.Controllers.Version1.Invoice
{
    [ApiVersion(ApiVersions.V2)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class InvoiceController(ISender sender) : BaseController
    {
        private readonly ISender _sender = sender;
        [HttpPost]
        public async Task<IActionResult> CreateInvoice(CreateInvoiceDto request
         , CancellationToken cancellation = default)
        {
            var response = await _sender.Send(new CreateInvoiceCommand(request), cancellation);
            return CreateResult(response);
        }

        [HttpGet("{InvoiceId}")]
        public async Task<IActionResult> GetInvoiceAsync(Guid InvoiceId, CancellationToken cancellation = default)
        {
            var response = await _sender.Send(
                new GetInvoiceQuery(InvoiceId), cancellation);
            return CreateResult(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInvoicetAsync(CancellationToken cancellation = default)
        {
            var response = await _sender.Send(
                new GetAllInvoicesQuery(), cancellation);
            return CreateResult(response);
        }

        [HttpPut("{InvoiceId}")]
        public async Task<IActionResult> UpdateInvoiceAsync(
            Guid InvoiceId,
            UpdateInvoiceDto request,
            CancellationToken cancellation = default)
        {
            var response = await _sender.Send(
                new UpdateInvoiceCommand(InvoiceId, request), cancellation);
            return CreateResult(response);
        }

        [HttpDelete("{InvoiceId}")]
        public async Task<IActionResult> DeleteInvoiceAsync(Guid InvoiceId,
           CancellationToken cancellation = default)
        {
            var response = await _sender.Send(
                new RemoveInvoiceCommand(InvoiceId), cancellation);
            return CreateResult(response);
        }

    }
}
