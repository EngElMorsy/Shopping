
using AECEMCore.Features.Invoices.Commands.UpdateInvoice;
using ECEMCore.Abstraction.Messaging.Commands;
using ECMDomain.Abstraction;
using ECMDomain.Entities.Invoicces;

namespace ECEMCore.Features.Invoices.Commands.UpdateInvoice
{
    internal sealed class UpdateInvoiceCommandHandler(IUnitWork unitWork) :
        ICommandHandler<UpdateInvoiceCommand>
    {
        private readonly IUnitWork _unitWork = unitWork;

        public async Task<Result<NoContentDto>> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var Invoicee = await _unitWork.Repostiry<Invoice>()
              .GetIdAsync(request.InvoiceId, cancellationToken);

            if (Invoicee is null)
                return Result<NoContentDto>
                    .Failed(400, "Null.Error", $"The Invoice with The Id :{request.InvoiceId}"); 

            Invoicee.Update(request.Dto); 

            _unitWork.Repostiry<Invoice>()
                .Update(Invoicee); 

            await _unitWork.CommitAsync(
             cancellationToken,
               chekForConcurrency: true);

            return Result<NoContentDto>
                .Success(204);


        }
    }
}
