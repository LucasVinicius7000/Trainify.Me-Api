namespace Trainify.Me_Api.Domain.DTOs
{
    public class UsuarioLogadoDTO
    {
        public string Email { get; set; }
        public string UserName { get; set; } = string.Empty;
        public dynamic Perfil { get; set; } 
        public bool IsActive { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiration { get; set; }
    }
}
