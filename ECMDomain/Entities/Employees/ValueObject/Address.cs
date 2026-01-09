using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMDomain.Entities.Employees
{
    public record Address(
        string FristLineAddress, 
        string SecondLineAddress, 
        string Postcode,
        string City,
        string Country);
 
}
