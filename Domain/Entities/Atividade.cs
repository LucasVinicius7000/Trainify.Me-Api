using System.ComponentModel.DataAnnotations.Schema;

namespace Trainify.Me_Api.Domain.Entities
{
    public class Atividade
    {
        public int Id { get; set; }
        public int AulaId { get; set; }
        public int AlternativaCorretaId { get; set; }
        public string Enunciado { get; set; }      
        

        [NotMapped]
        public List<Alternativa> Alternativas { get; set; }
        [NotMapped]
        public Alternativa AlternativaCorreta { get; set; }
        [NotMapped]
        public Aula AulaPertencente { get; set; }
    }
}
