using ECEMCore.Abstraction.Messaging.Commands;
using ECMDomain.Abstraction;
using ECMDomain.Entities.Identity.Users;
using Microsoft.AspNetCore.Identity;


namespace ECEMCore.Features.Identity.AppUsers.Commands.RevokeUser;
internal sealed class RevokeUserCommandHandler(
    UserManager<AppUser> userManager) : ICommandHandler<RevokeUserCommand, NoContentDto>
{
    private readonly UserManager<AppUser> _userManager = userManager;

    public async Task<Result<NoContentDto>> Handle(
        RevokeUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userManager
            .FindByEmailAsync(request.Dto.Email);

        if (user is null)
            return Result<NoContentDto>
                .Failed(400, new Error
                {
                    ErrorCode = "RevokeUser.Error",
                    ErrorMessages = ["User not exist"]
                });

        user.RevokeUser();

        await _userManager.UpdateAsync(user);

        return Result<NoContentDto>
            .Success(204);
    }
}
