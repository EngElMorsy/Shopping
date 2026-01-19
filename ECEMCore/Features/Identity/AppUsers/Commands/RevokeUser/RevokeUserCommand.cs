

using ECEMCore.Abstraction.Messaging.Commands;
using ECMDomain.Abstraction;
using ECMDomain.Entities.Identity.Users.DTOs;

namespace ECEMCore.Features.Identity.AppUsers.Commands.RevokeUser;
public record RevokeUserCommand(
    RevokeUserDto Dto) : ICommand<NoContentDto>;
