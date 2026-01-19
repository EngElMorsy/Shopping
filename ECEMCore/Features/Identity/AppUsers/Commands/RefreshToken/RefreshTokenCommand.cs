

using ECEMCore.Abstraction.Messaging.Commands;
using ECMDomain.Entities.Identity.Users.DTOs;

namespace ECEMCore.Features.Identity.AppUsers.Commands.RefreshToken;
public record RefreshTokenCommand(
    RefreshTokenDto Dto) : ICommand<RefreshTokenResponse>;