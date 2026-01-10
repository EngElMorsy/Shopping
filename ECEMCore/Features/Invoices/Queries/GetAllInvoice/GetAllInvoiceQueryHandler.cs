

using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECEMCore.Abstraction.Messaging.Queries;
using ECMDomain.Abstraction;
using ECMDomain.Entities.Invoicces;
using Microsoft.EntityFrameworkCore;

namespace ECEMCore.Features.Invoices.Queries.GetAllInvoice
{
      internal sealed class GetAllInvoiceQueryHandler(IUnitWork unitWork, IMapper mapper) :
        IQueryHandler<GetAllInvoicesQuery, InvoiceResponseCollection>
     {
        private readonly IUnitWork _unitWork = unitWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<InvoiceResponseCollection>> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
        {
                var invoices = await _unitWork.Repostiry<Invoice>()
               .GetAll()
               .ProjectTo<InvoiceResponse>(_mapper.ConfigurationProvider)
               .ToListAsync(cancellationToken);
             
            var response = new InvoiceResponseCollection
            {
                Invoices = invoices.AsReadOnly()
             };
               return Result<InvoiceResponseCollection>.Success(response, 200);
        }
    }
}
