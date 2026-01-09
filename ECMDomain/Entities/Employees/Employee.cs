using ECMDomain.Abstraction;
using ECMDomain.Entities.Employees.DTOs;
using ECMDomain.Entities.Employees.Events;
using ECMDomain.Entities.Invoicces;

namespace ECMDomain.Entities.Employees
{
    public sealed class Employee :BaseEntity
    {
        private Employee(
        Title title,
        Address address,
        Money balance)
        {
            Title = title;
            Address = address;
            Balance = balance;
        }

        private Employee() { }

        public Title Title { get; private set; } = null!;

        public Address Address { get; private set; } = null!;

        public Money Balance { get; private set; } = null!;

        // invoice will be locate here. Search_For_Needed
        public ICollection<Invoice> Invoice { get; private set; } = null!;
        public static Employee Create(CreateEmployeeDto dto)
        {

            //Before Use Dtos 
            #region Before Use DTOS  
             // public static Employee Create(Title title, Address address)
              //{ 
                //var employee = new Employee(title, address, new Money(0));

                //employee.RaiseDomainEvent(
                //new EmployeeCreateDomainEvent(employee.Id));

                //return employee;
              //}
             #endregion

      
            var employee = new Employee(
                new Title(dto.Title),
                new Address(
                    dto.FristLineAddress,
                    dto.SecondLineAddress,
                    dto.Postcode,
                    dto.City,
                    dto.Country),
                new Money(0));
            employee.RaiseDomainEvent(
                new EmployeeCreateDomainEvent(employee.Id));

            return employee;
        }

        public void update(UpdateEmployeeDto dto)
        { 
            Title =new Title(dto.Title);  
            Address=new Address(
                dto.FristLineAddress,
                dto.SecondLineAddress, 
                dto.Postcode,
                dto.City, 
                dto.Country);
        
        }

        //public void IncreaseBalance(Money invoiceAmount)
        //    => Balance = new Money(
        //        Balance.Value + invoiceAmount.Value);

        //public void DecreaseBalance(Money invoiceAmount)
        //    => Balance = new Money(
        //        Balance.Value - invoiceAmount.Value);

        //public void RemoveInvoice(Invoice invoice)
        //{
        //    Invoices.Remove(invoice);

        //    RaiseDomainEvent(new InvoiceRemovedDomainEvent(
        //        Id,
        //        invoice.TotalBalance));
        //}
    }
}
