

using AutoMapper;
using ECEMCore.Abstraction.Messaging.Commands;
using ECMDomain.Abstraction;
using ECMDomain.Entities.Invoicces;

namespace ECEMCore.Features.Invoices.Commands.CreateInvoice
{
    internal sealed class CreateInvoiceCommandHandler
        (IUnitWork unitWork, IMapper mapper) :
        ICommandHandler<CreateInvoiceCommand, InvoiceResponse>
    {
        private readonly IUnitWork _unitWork = unitWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<InvoiceResponse>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var Invoicee =  await Invoice.Create(request.Dto,_unitWork);
           
            await _unitWork.Repostiry<Invoice>()
                .CreateAsync(Invoicee,cancellationToken);
            await _unitWork.CommitAsync(cancellationToken); 
             var response=_mapper.Map<InvoiceResponse>(Invoicee);
            return Result<InvoiceResponse>
                .Success(response, 201);

        }
    }
}
