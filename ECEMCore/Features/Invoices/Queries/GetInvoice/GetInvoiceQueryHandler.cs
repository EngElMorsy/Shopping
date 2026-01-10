

using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECEMCore.Abstraction.Messaging.Queries;
using ECMDomain.Abstraction;
using ECMDomain.Entities.Invoicces;
using Microsoft.EntityFrameworkCore;

namespace ECEMCore.Features.Invoices.Queries.GetInvoice
{
    internal sealed class GetInvoiceQueryHandler
        (IUnitWork unitWork, IMapper mapper) : IQueryHandler<GetInvoiceQuery, InvoiceResponse>

    {
        private readonly IUnitWork _unitWork = unitWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<InvoiceResponse>> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
        {
            var response = await _unitWork.Repostiry<Invoice>()
                .GetAll()
                .Include(x => x.PurchasedProducts)
                .ProjectTo<InvoiceResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.InvoiceId, cancellationToken);

            if (response == null)
                return Result<InvoiceResponse>
                    .Failed(400, "Null.Error", $"The Invoice with The Id :{request.InvoiceId}");

            return Result<InvoiceResponse>
                      .Success(response, 200);
        }
    }
}
