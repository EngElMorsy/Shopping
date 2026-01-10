using ECEMCore.Abstraction.Messaging.Commands;
using ECMDomain.Entities.Invoicces;
namespace AECEMCore.Features.Invoices.Commands.UpdateInvoice;
public record UpdateInvoiceCommand(Guid InvoiceId,UpdateInvoiceDto Dto):ICommand;

