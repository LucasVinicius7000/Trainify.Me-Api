namespace Trainify.Me_Api.Domain.DTOs
{
    public class TokenDataDTO
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }   
        public DateTime? ExpirationRefreshToken { get; set; }
    }
}
