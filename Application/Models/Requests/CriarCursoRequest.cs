using Trainify.Me_Api.Domain.Entities;

namespace Trainify.Me_Api.Application.Models.Requests
{
    public class CriarCursoRequest
    {
        public string Nome { get; set; }
        public int OrganizacaoId { get; set; }
        public string UsuarioCriadorId { get; set; }

        public Curso ToCurso()
        {
            return new Curso()
            {
                Nome = Nome,
                OrganizacaoId = OrganizacaoId,
                UsuarioCriadorId = UsuarioCriadorId,
            };
        }
    }
}
