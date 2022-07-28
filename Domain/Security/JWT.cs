
namespace productMgtApi.Domain.Security
{
    public abstract class JWT
    {
        public string Token {get; protected set;}
        public DateTime Expiration {get; protected set;}

        public JWT(string token, DateTime expiration) {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException("Invalid token. ");
            }
            if (expiration <= DateTime.UtcNow) 
            {
                throw new ArgumentException("expired token. ");
            }
            Token = token;
            Expiration = expiration;
        
        }
        public bool IsExpired() => DateTime.UtcNow > Expiration;
    }
}