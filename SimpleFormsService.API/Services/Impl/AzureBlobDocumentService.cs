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

        public Task<string> UploadFiles(List<IFormFile> files, List<string> objectNames, string bucketName)
        {
            throw new NotImplementedException();
        }
    }
}
