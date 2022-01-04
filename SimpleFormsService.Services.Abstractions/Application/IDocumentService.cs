using Microsoft.AspNetCore.Http;
using Minio.DataModel;

namespace SimpleFormsService.Services.Abstractions.Application
{
    public interface IDocumentService
    {
        Task<ObjectStat> FindObject(string bucketName, string objectName, CancellationToken cancellationToken = default);
        Task<MemoryStream> GetObject(string bucketName, string objectName, CancellationToken cancellationToken = default);
        Task<List<string>> UploadFiles(List<IFormFile> files, string templateId, string submissionId, CancellationToken cancellationToken = default);
    }
}
