

using ECEMCore.Abstraction.Messaging.Commands;
using ECMDomain.Entities.Invoicces;

namespace ECEMCore.Features.Invoices.Commands.CreateInvoice
{
    public record CreateInvoiceCommand
        (CreateInvoiceDto Dto) :ICommand<InvoiceResponse>;
 
}
