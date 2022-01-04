using Microsoft.AspNetCore.Http;
using Minio;
using Minio.DataModel;
using SimpleFormsService.Services.Abstractions.Application;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleFormService.Test.Document
{
    public class DocumentServiceMockFactory : IDocumentService
    {
        public string bucketName { get; set; }
        public string objectName { get; set; }
        private readonly MinioClient client;
        

        public DocumentServiceMockFactory()
        {
            bucketName = "form-service";
            objectName = "4a62f048-3947-4ee7-904a-07b541d2c90f";
            client = new MinioClient("100.64.95.168:9000", "minioadmin", "minioadmin");
        }

        public async Task<ObjectStat> FindObject(string bucketName, string objectName, CancellationToken cancellationToken = default)
        {
            bucketName = bucketName;
            objectName = objectName;
            ObjectStat objectStat;
            objectStat = await client.StatObjectAsync(bucketName, objectName);
            return objectStat;
        }

        public async Task<MemoryStream> GetObject(string bucketName, string objectName, CancellationToken cancellationToken = default)
        {
            bucketName = bucketName;
            objectName = objectName;
            var responseStream = new MemoryStream();
            await client.GetObjectAsync(bucketName, objectName, (stream) =>
            {
                stream.CopyTo(responseStream);
                responseStream.Position = 0;
                stream.Dispose();
            });
            return responseStream;
        }

        public async Task<List<string>> UploadFiles(List<IFormFile> files, string bucketName, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}