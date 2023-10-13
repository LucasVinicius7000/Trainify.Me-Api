using Trainify.Me_Api.Domain.Entities;

namespace Trainify.Me_Api.Application.Models.Requests
{
    public class CriarUsuarioRequest
    {
        public string Nome { get; set; }
        public string? NomeDeUsuario { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Role { get; set; }
        public int OrganizacaoPertencenteId { get; set; } = 0;
        public int TreinadorDoAlunoId { get; set; } = 0;
    }

}
