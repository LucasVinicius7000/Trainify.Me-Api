using Microsoft.AspNetCore.Identity;


namespace Trainify.Me_Api.Domain.Entities
{
    public class Organizacao
    {
        public int Id { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string CNPJ { get; set; }

       
        public User User { get; set; }
        public List<Treinador> Treinadores { get; set; }
        public List<Aluno> Alunos { get; set; }
        
    }
}
