using Trainify.Me_Api.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trainify.Me_Api.Domain.Entities
{
    public class Aula
    {
        public int Id { get; set; }
        public int CursoId { get; set; }
        public int? AtividadeId { get; set; }  
        public int Indice { get; set; }
        public string Titulo { get; set; }
        public TipoAula TipoAula { get;set;}
        public string? VideoUrl { get; set; }
        public string? PDFUrl { get; set; }

        [NotMapped]
        public Curso Curso { get; set; }
        [NotMapped]
        public Atividade? Atividade { get; set; }

    }
}
