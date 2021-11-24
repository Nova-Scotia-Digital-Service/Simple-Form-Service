using Minio.DataModel;

namespace SimpleFormsService.API.Services
{
    public interface IDocumentService
    {
        Task<ObjectStat> FindObject(string bucketName, string objectName);
        Task<MemoryStream> GetObject(string bucketName, string objectName);
        Task<string> UploadFiles(List<IFormFile> files, List<string> objectNames, string bucketName);
        //Task RemoveObjects(List<IFormFile> files, List<string> objectNames);
    }
}
