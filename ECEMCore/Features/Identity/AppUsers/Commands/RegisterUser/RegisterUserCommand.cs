

using ECEMCore.Abstraction.Messaging.Commands;
using ECMDomain.Entities.Identity.Users.DTOs;

namespace ECEMCore.Features.Identity.AppUsers.Commands.RegisterUser;
public record RegisterUserCommand(
    RegisterUserDto Dto) : ICommand<RegisterUserResponse>;
