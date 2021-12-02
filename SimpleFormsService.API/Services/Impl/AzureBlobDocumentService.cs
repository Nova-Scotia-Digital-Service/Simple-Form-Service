using Minio.DataModel;

namespace SimpleFormsService.API.Services.Impl
{
    public class AzureBlobDocumentService : IDocumentService
    {
        public Task<ObjectStat> FindObject(string bucketName, string objectName)
        {
            throw new NotImplementedException();
        }

        public Task<MemoryStream> GetObject(string bucketName, string objectName)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> UploadFiles(List<IFormFile> files, string bucketName)
        {
            throw new NotImplementedException();
        }
    }
}
