

using System.ComponentModel.DataAnnotations;

namespace ECMDomain.Entities.Products.DTOs
{
    public class BaseProductDto
    {
        [Required]
        [MaxLength(45)]
        public string Description { get; set; } = null!;
       [Required]
        public decimal UnitPrice { get; set; } 
     

    }
    public class CreateProductDto : BaseProductDto;
    //public class UpdateProductDto : BaseProductDto
    //{ 
    //    public Guid ProductId { get; set; }
    //} 
    public class UpdateProductDto : BaseProductDto;



}




