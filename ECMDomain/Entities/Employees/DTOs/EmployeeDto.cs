

namespace ECMDomain.Entities.Employees.DTOs
{ 
    //Step 10 
    // After this Dto Go To Entity To Map Create method With This Dto
    public class BaseEmployeeDto
    {
       
        public string Title { get; set; } = null!;
      
        public string FristLineAddress { get; set; } = null!;
     
        public string SecondLineAddress { get; set; } = null!;
       
        public string Postcode { get; set; } = null!;
   
        public string City { get; set; } = null!;
        
        public string Country { get; set; } = null!;


        //[Required]
        //[MaxLength(45)]
        //public string Title { get; set; } = null!;
        //[Required]
        //[MaxLength(40)]
        //public string FristLineAddress { get; set; } = null!;
        //[MaxLength(40)]
        //public string SecondLineAddress { get; set; } = null!;
        //[Required]
        //[MaxLength(10)]
        //public string Postcode { get; set; } = null!;
        //[Required]
        //[MaxLength(20)]
        //public string City { get; set; } = null!;
        //[Required]
        //[MaxLength(20)]
        //public string Country { get; set; } = null!;
    }
    public class CreateEmployeeDto: BaseEmployeeDto;
    public class UpdateEmployeeDto : BaseEmployeeDto
    { 
        public Guid EmployeeId { get; set; }
    }
   // public class UpdateEmployeeDto : BaseEmployeeDto;

 

}
