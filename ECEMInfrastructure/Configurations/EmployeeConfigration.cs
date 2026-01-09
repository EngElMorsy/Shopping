using ECMDomain.Entities;
using ECMDomain.Entities.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace ECEMInfrastructure.Configurationss
{
    public class EmployeeConfigration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.OwnsOne(employee=> employee.Address, address=>
            {
                address.Property(address => address.FristLineAddress)
                .IsRequired()
                .HasMaxLength(40);

                address.Property(address => address.SecondLineAddress)
                .IsRequired()
                .HasMaxLength(40); 

                address.Property(address => address.Postcode)
                .IsRequired()
                .HasMaxLength(10);

                address.Property(address => address.City)
                .IsRequired()
                .HasMaxLength(20);

                address.Property(address => address.Country)
                .IsRequired()
                .HasMaxLength(20);

            });
            builder.Property(employee => employee.Title)
                .HasConversion(
                title => title.Value,
                value => new Title(value))
                .IsRequired()
                .HasMaxLength(45);

            builder.Property(employee => employee.Balance)
               .HasConversion(
               balance => balance.Value,
               value => new Money(value))
               .IsRequired()
               .HasPrecision(18, 2); 
            builder.HasMany(employee => employee.Invoice)
                .WithOne(x => x.Employee) 
                .HasForeignKey(x => x.EmployeeId) 
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.RowVersion)
                .IsRowVersion();
        }
    }
}
