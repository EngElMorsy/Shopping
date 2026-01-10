


using ECEMCore.Abstraction.Messaging.Commands;

namespace ECEMCore.Features.Invoices.Commands.RemoveInvoice
{
    public record RemoveInvoiceCommand(Guid InvoiceId) 
        :ICommand;
    
}
