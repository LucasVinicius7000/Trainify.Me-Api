using System.ComponentModel.DataAnnotations.Schema;
using Trainify.Me_Api.Domain.Enums;

namespace Trainify.Me_Api.Domain.Entities
{
    public class Aluno
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int TreinadorId { get; set; }
        public int OrganizacaoId { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataAdmissao { get; set; }
        public Sexo Sexo { get; set; }

        
        
        [NotMapped]
        public User User { get; set; }
        [NotMapped]
        public Treinador Treinador { get; set; }
        [NotMapped]
        public Organizacao Organizacao { get; set; }
    }
}
