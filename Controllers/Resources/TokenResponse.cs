namespace productMgtApi.Controllers.Resources
{
    public class TokenResponse
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}