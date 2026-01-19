using ECEMCore.Abstraction.Messaging.Commands;
using ECEMCore.Abstraction.TokenProviding;
using ECMDomain.Abstraction;
using ECMDomain.Entities.Identity.Users;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;


namespace ECEMCore.Features.Identity.AppUsers.Commands.LoginUser;
internal sealed class LoginUserCommandHandler(
    UserManager<AppUser> userManager,
    ITokenService tokenService) : ICommandHandler<LoginUserCommand, LoginUserResponse>
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<Result<LoginUserResponse>> Handle(
        LoginUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userManager
            .FindByEmailAsync(request.Dto.Email);

        if (user is null)
            return Result<LoginUserResponse>
                .Failed(400, new Error
                {
                    ErrorCode = "LoginFailed.Error",
                    ErrorMessages = ["Email or password not match"]
                });

        var isValidPassword = await _userManager
            .CheckPasswordAsync(user, request.Dto.Password);

        if (!isValidPassword)
            return Result<LoginUserResponse>
                .Failed(400, new Error
                {
                    ErrorCode = "LoginFailed.Error",
                    ErrorMessages = ["Email or password not match"]
                });

        var roles = await _userManager.GetRolesAsync(user);

        var accessToken = await _tokenService.CreateTokenAsync(user, roles);

        var refreshToken = _tokenService.GenerateRefreshToken();

        user.AddRefreshTokenInfo(
            refreshToken,
            _tokenService.GetRefreshTokenExpireDate());

        await _userManager.UpdateAsync(user);
        await _userManager.UpdateSecurityStampAsync(user);

        var token = new JwtSecurityTokenHandler().WriteToken(accessToken);

        var response = new LoginUserResponse
        {
            AccessToken = token,
            RefreshToken = refreshToken,
            ExpireDate = accessToken.ValidTo
        };

        return Result<LoginUserResponse>
            .Success(response, 200);
    }
}
