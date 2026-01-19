using AutoMapper;
using ECEMCore.Abstraction.Messaging.Commands;
using ECMDomain.Abstraction;
using ECMDomain.Entities.Identity.Users;
using Microsoft.AspNetCore.Identity;


namespace ECEMCore.Features.Identity.AppUsers.Commands.RegisterUser;

internal sealed class RegisterUserCommandHandler(
    UserManager<AppUser> userManager,
    IMapper mapper)
    : ICommandHandler<RegisterUserCommand, RegisterUserResponse>
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<RegisterUserResponse>> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await AppUser
            .Create(request.Dto, _userManager);

        var result = await _userManager.CreateAsync(user, request.Dto.Password);

        if (!result.Succeeded)
            return Result<RegisterUserResponse>
                .Failed(400, new Error
                {
                    ErrorCode = "RegisterFailed.Error",
                    ErrorMessages = result.Errors.Select(x => x.Description).ToList()
                });

        var response = _mapper.Map<RegisterUserResponse>(user);

        return Result<RegisterUserResponse>
            .Success(response, 200);
    }
}
