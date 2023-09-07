using System.ComponentModel.DataAnnotations.Schema;
using Trainify.Me_Api.Domain.Enums;

namespace Trainify.Me_Api.Domain.Entities
{
    public class Curso
    {
        public int Id { get; set; }
        public int OrganizacaoId { get; set; }
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public StatusCurso Status { get; set; }
        
        [NotMapped]
        public List<Aula> Aulas { get; set; }

        [NotMapped]
        public Organizacao OrganizacaoPertencente { get; set; }

        [NotMapped]
        public User UsuarioCriador { get; set; }
    }
}
