

using ECEMCore.Abstraction.Messaging.Commands;
using ECMDomain.Abstraction;
using ECMDomain.Entities.Invoicces;

namespace ECEMCore.Features.Invoices.Commands.RemoveInvoice
{
    internal sealed class RemoveInvoiceCommandHandler
        (IUnitWork unitWork) : ICommandHandler<RemoveInvoiceCommand>
    {
        private readonly IUnitWork _unitWork = unitWork;

        public async Task<Result<NoContentDto>> Handle(
            RemoveInvoiceCommand request,
            CancellationToken cancellationToken)
        {
            var Invoicee = await _unitWork.Repostiry<Invoice>()
             .GetIdAsync(request.InvoiceId,cancellationToken);

            if (Invoicee is null)
                return Result<NoContentDto>
                    .Failed(400, "Null.Error", $"The Invoice with The Id :{request.InvoiceId}");

            _unitWork.Repostiry<Invoice>().Delete(Invoicee); 

            await _unitWork.CommitAsync(cancellationToken); 

            return Result<NoContentDto>
                .Success(204);
        }
    }
}
