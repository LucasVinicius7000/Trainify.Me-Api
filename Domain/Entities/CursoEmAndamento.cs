using Trainify.Me_Api.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trainify.Me_Api.Domain.Entities
{
    public class CursoEmAndamento
    {
        public int Id { get; set; }
        public int AlunoId { get; set; }
        public int CursoBaseId { get; set; }
        public int? AulaAtualId { get; set; }
        public int? UserIdMatriculador { get; set; }
        public string? ProgressoAulaAtual { get; set; }
        public DateTime MatriculadoEm { get; set; }
        public DateTime? IniciadoEm { get; set; }
        public DateTime? ConcluidoEm { get; set; }
        public StatusCursoAndamento StatusCurso { get; set; }
        [NotMapped]
        public Curso CursoBase { get; set; }

    }
}
