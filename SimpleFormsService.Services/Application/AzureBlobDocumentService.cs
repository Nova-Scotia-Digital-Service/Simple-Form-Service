﻿using Microsoft.AspNetCore.Http;
using SimpleFormsService.Domain.Entities.Supporting;
using SimpleFormsService.Services.Abstractions.Application;

namespace SimpleFormsService.Services.Application
{
    public class AzureBlobDocumentService : ServiceBase, IDocumentService
    {
        public Task<FileStreamResultAdapter> GetObject(string bucketName, string objectName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> UploadFiles(string bucketName, List<IFormFile> files, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveFile(string bucketName, string objectName, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> UploadFiles(List<IFormFile> files, string templateId, string submissionId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> UploadFiles(List<IFormFile> files, string bucketName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }      
    }
}
