using AutoMapper;
using ECMDomain.Abstraction;
using ECMDomain.Entities.Identity.Users;


namespace ECEMCore.Features.Identity.AppUsers.Commands.RegisterUser;
public class RegisterUserResponse : IResult
{
    public Guid Id { get; set; }
    public string Fullname { get; set; } = null!;
    public string Email { get; set; } = null!;    
}

public class RegisterUserMapper : Profile
{
    public RegisterUserMapper()
        => CreateMap<AppUser, RegisterUserResponse>();
}
