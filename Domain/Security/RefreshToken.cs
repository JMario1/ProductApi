namespace productMgtApi.Domain.Security
{
    public class RefreshToken : JWT
    {
        public RefreshToken(string token, DateTime expiration) : base(token, expiration)
        {}
    }
}