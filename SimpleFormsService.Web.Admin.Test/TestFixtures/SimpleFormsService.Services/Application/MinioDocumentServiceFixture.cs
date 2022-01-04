using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using Minio.Exceptions;
using RestSharp.Serialization;
using SimpleFormsService.Persistence;
using SimpleFormsService.Services.Abstractions;
using SimpleFormsService.Test.SharedFixtures;
using Xunit;

namespace SimpleFormsService.Test.TestFixtures.SimpleFormsService.Services.Application
{
    [Collection(Constants.ConcreteDatabaseTestCollectionName)]
    public class MinioDocumentServiceFixture : IClassFixture<ConcreteDatabaseSharedFixture<SimpleFormsServiceDbContext>>
    {
        private readonly ConcreteDatabaseSharedFixture<SimpleFormsServiceDbContext> _sharedFixture;
        private readonly IServiceManager _serviceManager;
        private readonly MinioClient _client;

        public MinioDocumentServiceFixture(ConcreteDatabaseSharedFixture<SimpleFormsServiceDbContext> sharedFixture)
        {
            _sharedFixture = sharedFixture;
            _serviceManager = sharedFixture.Container.GetService<IServiceManager>()!;
            _client = sharedFixture.Container.GetService<MinioClient>()!;
        }

        [Fact]
        public async void UploadFiled_WhenCalled_GoodStuffHappens()
        {
            var formTemplate = _sharedFixture.CreateFormTemplate();
            var formSubmission = _sharedFixture.CreateFormSubmission(formTemplate);

            const string filePath = "C:\\Users\\Craig\\Downloads\\seuss.pdf";
            var fileName = filePath.Split(@"\").Last();

            await using var stream = new MemoryStream((await File.ReadAllBytesAsync(filePath)).ToArray());

            var formFile = new FormFile(stream, 0, stream.Length, "streamFile", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/pdf"
            };

            var objectNames = await _serviceManager.MinIoDocumentService.UploadFiles(new List<IFormFile>{ formFile }, formTemplate.Id.ToString(), formSubmission.Id.ToString());

            Assert.True(objectNames.Count == 1);
        }
    }
}