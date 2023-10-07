using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Trainify.Me_Api.Domain.Enums;
using System.ComponentModel;
using Trainify.Me_Api.Services.Extensions;

namespace Trainify.Me_Api.Infra.Services.BlobStorage
{
    public class BlobStorageService : IBlobStorageService
    {

        private readonly IConfiguration _configuration;
        private BlobServiceClient Client { get; }
        private BlobContainerClient Container { get; }

        public BlobStorageService(IConfiguration configuration)
        {
            _configuration = configuration;

            Client = new BlobServiceClient(
                _configuration.GetConnectionString("AzureStorage"));

            Container = Client.GetBlobContainerClient(
                _configuration.GetValue<string>("AzureBlobContainer"));
        }

        public async Task<string> UploadFile(string fileName, string fileExtension, MimeType? mimeType, Stream content)
        {
            try
            {
                if (mimeType is null) throw new Exception("A extensão do arquivo não é suportada.");
                BlobClient blobClient = Container.GetBlobClient(fileName + "." + fileExtension);
                var result = await blobClient.UploadAsync(content, new BlobHttpHeaders { ContentType = EnumExtensions.GetDescription(mimeType) });
                return Container.GetBlobClient(fileName + "." + fileExtension).Uri.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao realizar o upload do arquivo " + fileName + fileExtension + " : " + ex.Message);
            }
        }

    }
}
