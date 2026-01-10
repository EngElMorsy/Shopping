using AutoMapper;
using ECMDomain.Abstraction;
using ECMDomain.Entities.Employees;

namespace ECEMCore.Features.Employees
{
    public class EmployeeResponse : IResult
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public Address Address { get; set; } = null!;
        public decimal Balance { get; set; }
    }
    public class EmployeeResponseCollection : IResult
    {
        public IReadOnlyCollection<EmployeeResponse> Employees { get; set; } = null!;
    }
    public class EmployeeMapper : Profile
    {
        public EmployeeMapper()
        {
            CreateMap<Employee, EmployeeResponse>()
              .ForMember(dto => dto.Title, opt => opt.MapFrom(ent => ent.Title.Value))
              .ForMember(dto => dto.Balance, opt => opt.MapFrom(ent => ent.Balance.Value));
        }
    }

}
