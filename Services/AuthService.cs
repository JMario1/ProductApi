
using productMgtApi.Domain.Models;
using productMgtApi.Domain.Security;
using productMgtApi.Domain.Services;

namespace productMgtApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAppUserService _userService;
        private readonly ITokenHandler _tokenHandler;

        public AuthService(IAppUserService userService, ITokenHandler tokenHandler)
        {
            _userService = userService;
            _tokenHandler = tokenHandler;
        }

        public async Task<Response<AccessToken>> CreateAccessTokenAsync(string email, string password)
        {
            AppUser user = await _userService.FindByEmailAsync(email);
            if (user == null || !await _userService.CheckPasswordAsync(email, password))
            {
                return new Response<AccessToken>(false, "invalid credentials", null);
            }

            AccessToken token = _tokenHandler.CreateAccessToken(user);
            return new Response<AccessToken>(true, "success", token);
        }

        public async Task<Response<AccessToken>> RefreshTokenAsync(string refreshToken, string email)
        {
             RefreshToken token = _tokenHandler.TakeRefreshToken(refreshToken);

            if (token == null)
            {
                return new Response<AccessToken>(false, "Invalid refresh token.", null);
            }

            if (token.IsExpired())
            {
                return new Response<AccessToken>(false, "Expired refresh token.", null);
            }

            AppUser user = await _userService.FindByEmailAsync(email);
            if (user == null)
            {
                return new Response<AccessToken>(false, "Invalid Email.", null);
            }

            AccessToken accessToken = _tokenHandler.CreateAccessToken(user);
            return new Response<AccessToken>(true, "success", accessToken);
        }

        public void RevokeRefreshToken(string refreshToken)
        {
            _tokenHandler.RevokeRefreshToken(refreshToken);
        }
    }
}