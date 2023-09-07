using Trainify.Me_Api.Domain.Enums;

namespace Trainify.Me_Api.Domain.Entities
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataAdmissao { get; set; }
        public Sexo Sexo { get; set; }

        // Propriedades de navegação e FKs
        public User User { get; set; }

        public int TreinadorId { get; set; }
        public Treinador Treinador { get; set; }

        public int OrganizacaoId { get; set; }
        public Organizacao Organizacao { get; set; }
    }
}
