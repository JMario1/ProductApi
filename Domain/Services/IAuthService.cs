
using productMgtApi.Domain.Security;
using productMgtApi.Domain.Services;

namespace productMgtApi.Domain.Services
{
    public interface IAuthService
    {
        Task<Response<AccessToken>> CreateAccessTokenAsync(string email, string password);
        Task<Response<AccessToken>> RefreshTokenAsync(string refreshToken, string email);
        void RevokeRefreshToken(string refreshToken);
    }
}