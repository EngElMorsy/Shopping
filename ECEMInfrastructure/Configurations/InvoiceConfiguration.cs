
using ECMDomain.Entities;
using ECMDomain.Entities.Invoicces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECEMInfrastructure.Configurationss
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.Property(invoice => invoice.PoNumber)
                .HasConversion(
                 poNumber => poNumber.Value,
                 value => new PoNumber(value))
                .IsRequired()
                .HasMaxLength(45);

            builder.Property(invoice => invoice.TotalBalance)
                .HasConversion(
                totalBalance => totalBalance.Value,
                value => new Money(value))
                .IsRequired()
                .HasPrecision(18, 2);

            builder.HasMany(invoice => invoice.PurchasedProducts)
                .WithOne(x => x.Invoices)
                .HasForeignKey(x => x.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.RowVersion)
                .IsRowVersion();
        }
    }
}
