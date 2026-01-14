
using ECMDomain.Entities.InvoiceItems.DTOs;
using System.ComponentModel.DataAnnotations;


namespace ECMDomain.Entities.Invoicces
{
    public abstract class BaseInvoiceDto
    {
        [Required]
        [MaxLength(45)]
        public string PoNumber { get; set; } = null!;
    }
    public class CreateInvoiceDto : BaseInvoiceDto
    {
        [Required]
        public Guid EmployeeId { get; set; }
        [Required]
        public ICollection<CreateInvoiceItemDto> PurchasedProducts { get; set; } = null!;
    }
    public class UpdateInvoiceDto : BaseInvoiceDto;

}
