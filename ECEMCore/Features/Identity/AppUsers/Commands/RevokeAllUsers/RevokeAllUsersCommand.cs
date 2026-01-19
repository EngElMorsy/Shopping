

using ECEMCore.Abstraction.Messaging.Commands;
using ECMDomain.Abstraction;
using ECMDomain.Entities.Identity.Users.DTOs;

namespace ECEMCore.Features.Identity.AppUsers.Commands.RevokeAllUsers;
public record RevokeAllUsersCommand(
    RevokeAllUsersDto Dto) : ICommand<NoContentDto>;
