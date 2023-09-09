using System.ComponentModel.DataAnnotations.Schema;

namespace Trainify.Me_Api.Domain.Entities
{
    public class Alternativa
    {
        public int Id { get; set; }
        public int AtividadeId { get; set; }
        public string Valor { get; set; }

        [NotMapped]
        public Atividade Atividade { get; set; }
    }
}
