using Trainify.Me_Api.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trainify.Me_Api.Domain.Entities
{
    public class Treinador
    {
        public int Id { get; set; }
        public int OrganizacaoId { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get;set; }
        public Sexo Sexo { get; set; }


   

        [NotMapped]
        public User User { get; set; }

        [NotMapped]
        public List<Aluno> Alunos { get; set; }

        [NotMapped]
        public Organizacao Organizacao { get; set; }
        
    }
}
