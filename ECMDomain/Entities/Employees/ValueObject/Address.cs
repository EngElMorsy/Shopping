

namespace ECMDomain.Entities.Employees
{
    public record Address(
        string FristLineAddress, 
        string SecondLineAddress, 
        string Postcode,
        string City,
        string Country);
 
}
