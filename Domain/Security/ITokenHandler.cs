using productMgtApi.Domain.Models;

namespace productMgtApi.Domain.Security
{
    public interface ITokenHandler
    {
        AccessToken CreateAccessToken(AppUser user);
        RefreshToken TakeRefreshToken(string token);
        void RevokeRefreshToken(string token);
    }
}