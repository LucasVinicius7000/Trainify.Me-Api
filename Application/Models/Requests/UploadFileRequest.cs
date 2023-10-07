using Trainify.Me_Api.Domain.Enums;

namespace Trainify.Me_Api.Application.Models.Requests
{
    public class UploadFileRequest
    {

        public string NomeArquivo { get; set; }
        public string ExtensaoArquivo { get; set; }
        public string Base64Arquivo { get; set; }
        public MimeType? MimeType { get; set; } = null;

    }
}
