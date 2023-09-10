using System.ComponentModel.DataAnnotations;

namespace Trainify.Me_Api.Application.Models.Requests
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public string NomeUsuario { get; set; }
    }
}
