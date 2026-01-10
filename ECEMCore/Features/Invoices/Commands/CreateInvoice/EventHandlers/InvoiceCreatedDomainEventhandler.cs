

using ECEMCore.Abstraction.Emailing;
using ECMDomain.Abstraction;
using ECMDomain.Entities.Invoicces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECEMCore.Features.Invoices.Commands.CreateInvoice.EventHandlers
{
    internal sealed class InvoiceCreatedDomainEventhandler(IUnitWork unitWork ,
        IEmailService emailService) :
        INotificationHandler<InvoiceCreatedDomainEvent>
    {
        private readonly IUnitWork _unitWork = unitWork;
        private readonly IEmailService _emailService = emailService;

        public async Task Handle(InvoiceCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var Invoicee = await _unitWork.Repostiry<Invoice>()
                .GetAll()
                .AsTracking()
                .Include(x => x.Employee)
                .FirstOrDefaultAsync(x=>x.Id== notification.invoiceId,cancellationToken);
            

            if (Invoicee is null)
                return;  //Search  

            //for UpdateBalance for employeeOr Customer
            Invoicee.Employee.UpdateBalance(Invoicee.TotalBalance);

             _unitWork.Repostiry<Invoice>().Update(Invoicee);
            await _unitWork.CommitAsync(cancellationToken); 

        }
    }
}
