using Trainify.Me_Api.Application.Controllers.Shared;
using Trainify.Me_Api.Application.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Trainify.Me_Api.Application.Controllers
{
    public class CursoController : BaseController<CursoController>
    {
        public CursoController(IServiceProvider serviceProvider) : base(serviceProvider) { }

        //[HttpPost("teste")]
        //public async Task<string> TesteUploadArquivo([FromBody] UploadFileRequest file)
        //{
        //    var content = new MemoryStream(Convert.FromBase64String(file.Base64Arquivo));
        //    var arquivoUrl = await BlobStorage.UploadFile(file.NomeArquivo, file.ExtensaoArquivo, file.MimeType, content);
        //    return arquivoUrl;
        //}

    }
}
