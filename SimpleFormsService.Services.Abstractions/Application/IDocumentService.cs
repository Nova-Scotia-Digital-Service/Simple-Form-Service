using Microsoft.AspNetCore.Http;
using SimpleFormsService.Domain.Entities.Supporting;

namespace SimpleFormsService.Services.Abstractions.Application;

public interface IDocumentService
{
    Task<FileStreamResultAdapter> GetObject(string bucketName, string objectName, CancellationToken cancellationToken = default);
    Task<List<string>> UploadFiles(string bucketName, List<IFormFile> files, CancellationToken cancellationToken = default);
    Task<bool> RemoveFile(string bucketName, string objectName, CancellationToken cancellationToken = default);
}