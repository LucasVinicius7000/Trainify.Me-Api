namespace Trainify.Me_Api.Domain.Entities
{
    public class Atividade
    {
        public int Id { get; set; }
        public int AulaId { get; set; }
        public string Titulo { get; set; }
        public string EnunciadoQuestao { get; set; }
        //public List<string> ListaAlternativas { get; set; }
        //public string AlternativaCorreta { get; set; }
        public Aula AulaPertencente { get; set; }
    }
}
