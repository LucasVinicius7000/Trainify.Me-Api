using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trainify.Me_Api.Domain.Entities
{
    public class Organizacao
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string CNPJ { get; set; }


        [NotMapped]
        public User User { get; set; }
        [NotMapped]
        public List<Treinador> Treinadores { get; set; }
        [NotMapped]
        public List<Aluno> Alunos { get; set; }
        [NotMapped]
        public List<Curso> Cursos { get; set; }
        
    }
}
