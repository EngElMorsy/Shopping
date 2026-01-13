using ECMDomain.Abstraction;
using ECMDomain.Entities.Employees;
using ECMDomain.Entities.InvoiceItems;
using ECMDomain.Entities.InvoiceItems.ValueObjects;
using ECMDomain.Entities.Products;
using ECMDomain.Exceptions;


namespace ECMDomain.Entities.Invoicces
{
    public sealed class Invoice:BaseEntity
    {

        private Invoice() { }

        private Invoice(Guid invoiceId,
            PoNumber poNumber,
            Money totalBalance,
            Guid employeeId,
            ICollection<InvoiceItem> purchasedProducts) : base(invoiceId)
        {
            PoNumber = poNumber;
            TotalBalance = totalBalance;
            EmployeeId = employeeId;
            PurchasedProducts = purchasedProducts;
        }





        public PoNumber PoNumber { get; private set; } = null!;
        public Money TotalBalance { get; private set; } = null!;
        public Guid EmployeeId { get; private set; }
        public Employee Employee { get; private set; } = null!;

        public ICollection<InvoiceItem> PurchasedProducts { get; private set; } = null!;

        public static async Task<Invoice> Create(CreateInvoiceDto dto, IUnitWork unitWork)
        {

            if (dto.PurchasedProducts is null || dto.PurchasedProducts.Count == 0)
                //**BeFore Cutom Expection Type 
               // throw new InvalidOperationException("Empty Invoice can not be created"); 
                throw new BadRequestException(["Empty Invoice can not be created"]); 


            var invoiceId = Guid.NewGuid();
            ICollection<InvoiceItem> PurchasedProducts = [];

            foreach (var purchasedProduct in dto.PurchasedProducts)
            {
                var product = await unitWork
                    .Repostiry<Product>()
                    .GetIdAsync(purchasedProduct.ProductId) ??
                    //**BeFore Cutom Expection Type
                    //throw new ArgumentNullException($"Product with id: {purchasedProduct.ProductId} not found");
                    throw new NullObjectException([$"Product with id: {purchasedProduct.ProductId} not found"]);

                 var invoiceItem = new InvoiceItem(
                    Guid.NewGuid(),
                    //new Title(product.Description.Value),
                    //new Money(product.UnitPrice.Value),
                    product.Description,
                     product.UnitPrice,
                    new Quantity(purchasedProduct.Quantity),
                    invoiceId
                    );
                PurchasedProducts.Add(invoiceItem);

            }
            var totalBalance = PurchasedProducts.Sum(x => x.TotalPrice.Value);
            var invoice = new Invoice(invoiceId,
                 new PoNumber(dto.PoNumber),
                new Money(totalBalance),
                 dto.EmployeeId,
                  PurchasedProducts);
            invoice.RaiseDomainEvent(new InvoiceCreatedDomainEvent(invoiceId));

            return invoice;
            //if (dto.EmployeeId == Guid.Empty)
            //    throw new BadRequestException(
            //        ["Customer Id is required"]);

            //if (dto.PurchasedProducts is null || dto.PurchasedProducts.Count == 0)
            //    throw new BadRequestException(
            //        ["Empty Invoice can not be created"]);

            //if (dto.PurchasedProducts.Any(x => x.ProductId == Guid.Empty))
            //    throw new BadRequestException(
            //        ["Product Id(s) is/are missing in your purchased product list"]);

            //if (dto.PurchasedProducts.Any(x => x.Quantity <= 0))
            //    throw new BadRequestException(
            //        ["Product Quantity must be set and must be positive number in your purchased product list"]);

        }
        public void Update(UpdateInvoiceDto dto)
        {
            PoNumber = new PoNumber(dto.PoNumber);
        }
    }
}
