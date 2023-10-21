using Trainify.Me_Api.Domain.Enums;

namespace Trainify.Me_Api.Application.Models.Requests
{
    public class AdicionarAulaRequest
    {
        public int CursoId { get; set; }
        public int IndiceAula { get; set; }
        public string? TituloAula { get; set; }
        public TipoAula TipoAula { get; set; }
        public UploadFileRequest? DadosAula { get; set; } = null;
        public string? EnunciadoQuestao { get; set; } = string.Empty;
        public string[]? Alternativas { get; set; } = null;
        public string? AlternativaCorreta { get; set; } = string.Empty;

    }
}
