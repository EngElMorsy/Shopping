

using ECMDomain.Abstraction;
using ECMDomain.Entities.Products.DTOs;

namespace ECMDomain.Entities.Products
{
    public sealed class Product :BaseEntity
    {
        public Title Description { get; private set; } = null!;
        public Money UnitPrice { get; private set; } = null!;

        private Product()
        {
             
        }

        private Product(Guid id, Title description, Money unitPrice) : base(id)
        {
            Description = description;
            UnitPrice = unitPrice;
        }

        public static Product Create(CreateProductDto dto)
         => new (
             Guid.NewGuid(),
           new Title(dto.Description),
          new Money(dto.UnitPrice));

        public void Update(UpdateProductDto dto)
        {
            Description = new Title(dto.Description);
            UnitPrice = new Money(dto.UnitPrice);
        }
    }
}
