namespace productMgtApi.Domain.Security
{
    public class AccessToken : JWT
    {
        public RefreshToken RefreshToken {get; private set;}
        public AccessToken(string token, DateTime expiration, RefreshToken refreshToken) : base(token, expiration) 
        {
            if (refreshToken == null) throw new ArgumentException("Specify a valid refresh token");
            RefreshToken = refreshToken;
        }
    }
}