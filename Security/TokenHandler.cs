using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using productMgtApi.Domain.Models;
using productMgtApi.Domain.Security;
using productMgtApi.Domain.Services;

namespace productMgtApi.Security
{
    public class TokenHandler : ITokenHandler
    {
        private static readonly ISet<RefreshToken> _refreshTokens = new HashSet<RefreshToken>();
        private readonly IConfiguration _configuration;
        private readonly IAppUserService _userService;

        public TokenHandler(IConfiguration configuration, IAppUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        public AccessToken CreateAccessToken(AppUser user)
        {
            RefreshToken refreshToken = BuildRefreshToken();
            AccessToken accessToken = BuildAccessTokenAsync(user, refreshToken).GetAwaiter().GetResult();
            _refreshTokens.Add(refreshToken);

            return accessToken;
        }

        public void RevokeRefreshToken(string token)
        {
            TakeRefreshToken(token);
        }

        public RefreshToken TakeRefreshToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return null!;
            RefreshToken? refreshToken = _refreshTokens.SingleOrDefault(t => t.Token == token);
            if (refreshToken != null)
                _refreshTokens.Remove(refreshToken);

            return refreshToken!;
        }

        private async Task<AccessToken> BuildAccessTokenAsync(AppUser user, RefreshToken refreshToken)
        {
            DateTime accessTokenExpiration = DateTime.UtcNow.AddSeconds(_configuration.GetValue<int>("TokenOptions:AccessTokenExpiration"));

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email)
            };

            IList<string> roles = await _userService.GetRoleAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            SymmetricSecurityKey authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("TokenOptions:Secret")));  


            var securityToken = new JwtSecurityToken
            (
                issuer : _configuration.GetValue<string>("TokenOptions:Issuer"),
                audience : _configuration.GetValue<string>("TokenOptions:Audience"),
                claims : claims,
                expires : accessTokenExpiration,
                notBefore : DateTime.UtcNow,
                signingCredentials : new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            string accessToken = handler.WriteToken(securityToken);

            return new AccessToken(accessToken, securityToken.ValidTo, refreshToken);
        }

        private RefreshToken BuildRefreshToken()
        {
            SHA256 sha = SHA256.Create();
            RefreshToken refreshToken = new RefreshToken
            (
                token : BitConverter.ToString(sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()))).Replace("-", string.Empty),
                expiration : DateTime.UtcNow.AddSeconds(_configuration.GetValue<int>("TokenOptions:RefreshTokenExpiration"))
            );

            return refreshToken;
        }
    }
}