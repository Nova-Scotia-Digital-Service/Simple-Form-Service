using Minio.DataModel;

namespace SimpleFormsService.API.Services
{
    public interface IDocumentService
    {
        Task<ObjectStat> FindObject(string bucketName, string objectName);
        Task<MemoryStream> GetObject(string bucketName, string objectName);
        Task<List<string>> UploadFiles(List<IFormFile> files, string bucketName); //TODO: param not yet decided
        //Task RemoveObj>ects(List<IFormFile> files, List<string> objectNames);
    }
}
