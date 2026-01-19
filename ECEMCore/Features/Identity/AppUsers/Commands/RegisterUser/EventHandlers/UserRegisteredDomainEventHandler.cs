using ECMDomain.Abstraction;
using ECMDomain.Entities.Identity.Roles;
using ECMDomain.Entities.Identity.Users;
using ECMDomain.Entities.Identity.Users.Events;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace ECEMCore.Features.Identity.AppUsers.Commands.RegisterUser.EventHandlers;
internal sealed class UserRegisteredDomainEventHandler(
    UserManager<AppUser> userManager)
    : INotificationHandler<UserRegisteredDomainEvent>
{
    private readonly UserManager<AppUser> _userManager = userManager;

    public async Task Handle(
        UserRegisteredDomainEvent notification, 
        CancellationToken cancellationToken)
    {
        var user = await _userManager
            .FindByIdAsync(notification.UserId.ToString());

        if (user is null)
            return; // Search

        if (string.IsNullOrEmpty(notification.AdminKey))        
            await _userManager.AddToRoleAsync(user, AppRole.User.Name!);
        else
        {
            if (!notification.AdminKey.Equals(AppRole.ADMIN_KEY))
                throw new AdminKeyNotMatchException(
                    ["Admin key not matched"]);

            await _userManager.AddToRoleAsync(user, AppRole.Admin.Name!);
        }
           
    }
}
