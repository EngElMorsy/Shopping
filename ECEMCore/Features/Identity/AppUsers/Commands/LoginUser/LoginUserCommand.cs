

using ECEMCore.Abstraction.Messaging.Commands;
using ECMDomain.Entities.Identity.Users.DTOs;

namespace ECEMCore.Features.Identity.AppUsers.Commands.LoginUser;
public record LoginUserCommand(
    LoginUserDto Dto) : ICommand<LoginUserResponse>;
