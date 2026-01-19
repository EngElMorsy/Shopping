using ECEMCore.Abstraction.Messaging.Commands;
using ECMDomain.Abstraction;
using ECMDomain.Entities.Identity.Roles;
using ECMDomain.Entities.Identity.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECEMCore.Features.Identity.AppUsers.Commands.RevokeAllUsers;
internal sealed class RevokeAllUsersCommandHandler(
    UserManager<AppUser> userManager,
    RoleManager<AppRole> roleManager) : ICommandHandler<RevokeAllUsersCommand, NoContentDto>
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly RoleManager<AppRole> _roleManager = roleManager;

    public async Task<Result<NoContentDto>> Handle(
        RevokeAllUsersCommand request,
        CancellationToken cancellationToken)
    {
        IList<AppUser> users;

        if (string.IsNullOrEmpty(request.Dto.Role))
            users = await _userManager.Users
                .ToListAsync(cancellationToken);
        else
        {
            var role = await _roleManager.FindByNameAsync(request.Dto.Role);

            if (role is null)
                return Result<NoContentDto>
                    .Failed(400, new Error
                    {
                        ErrorCode = "RevokeAllFailed.Error",
                        ErrorMessages = [$"{request.Dto.Role} role not exist"]
                    });

            users = await _userManager
                .GetUsersInRoleAsync(request.Dto.Role);
        }        

        foreach (var user in users)
        {
            user.RevokeUser();

            await _userManager.UpdateAsync(user);
        }

        return Result<NoContentDto>
            .Success(204);
    }
}
