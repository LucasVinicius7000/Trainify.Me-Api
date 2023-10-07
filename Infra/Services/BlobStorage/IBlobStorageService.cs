using Trainify.Me_Api.Domain.Enums;

namespace Trainify.Me_Api.Infra.Services.BlobStorage
{
    public interface IBlobStorageService
    {
        Task<string> UploadFile(string fileName, string fileExtension, MimeType? mimeType, Stream content);
    }
}
