

using ECMDomain.Entities;
using ECMDomain.Entities.InvoiceItems;
using ECMDomain.Entities.InvoiceItems.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECEMInfrastructure.Configurationss
{
    public class InvoiceItemConfiguration : IEntityTypeConfiguration<InvoiceItem>
    {
        public void Configure(EntityTypeBuilder<InvoiceItem> builder)
        {
            builder.Property(item => item.SellPrice)
                .HasConversion(
                 sellPrice => sellPrice.Value,
                 value => new Money(value))
                .IsRequired()
                .HasPrecision(18,2);



            builder.Property(item => item.TotalPrice)
                .HasConversion(
                 totalPrice => totalPrice.Value,
                 value => new Money(value))
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(item => item.Quantity)
               .HasConversion(
                quantity => quantity.Value,
                value => new Quantity(value))
               .IsRequired();

            builder.Property(x => x.RowVersion)
             .IsRowVersion();

            builder.Property(item => item.Description)
           .HasConversion(
               description => description.Value,
               value => new Title(value))
           .IsRequired()
           .HasMaxLength(45);
        }
    }
}
