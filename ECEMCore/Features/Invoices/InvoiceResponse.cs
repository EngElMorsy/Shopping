

using AutoMapper;
using ECEMCore.Features.Employees;
using ECMDomain.Abstraction;
using ECMDomain.Entities.Invoicces;

namespace ECEMCore.Features.Invoices
{
    public class InvoiceResponse :IResult
    { 

        public Guid Id { get; set; }
        public string PoNumber { get; set; } = null!;
        public EmployeeResponse Employee { get; set; } = null!;  
        public decimal InvoiceBalance { get; set; }
        public ICollection<InvoiceItemResponse> PurchasedProducts { get; set; } = null!;
    }
    public class InvoiceResponseCollection : IResult
    {
        public IReadOnlyCollection<InvoiceResponse> Invoices { get; set; } = null!;
    }

    public class InvoiceMapper:Profile
    {
        public InvoiceMapper()
        {
            CreateMap<Invoice, InvoiceResponse>()
                .ForMember(dto => dto.PoNumber, opt => opt.MapFrom(ent => ent.PoNumber.Value))
                .ForMember(dto => dto.Employee, opt => opt.MapFrom(ent => ent.Employee))
                .ForMember(dto => dto.InvoiceBalance, opt => opt.MapFrom(ent => ent.TotalBalance.Value));
      

        }
    }
     
 }
