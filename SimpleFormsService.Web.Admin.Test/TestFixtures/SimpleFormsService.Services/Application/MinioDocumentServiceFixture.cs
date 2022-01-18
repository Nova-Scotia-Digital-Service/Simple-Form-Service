using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.DependencyInjection;
using SimpleFormsService.Domain.Exceptions;
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

        public MinioDocumentServiceFixture(ConcreteDatabaseSharedFixture<SimpleFormsServiceDbContext> sharedFixture)
        {
            _sharedFixture = sharedFixture;
            _serviceManager = sharedFixture.Container.GetService<IServiceManager>()!;
        }

        [Fact]
        public async void UploadFiles_WhenCalledWithAnEmptyFileList_ANullOrEmptyListExceptionShouldBeThrown()
        {
            var formTemplate = _sharedFixture.CreateFormTemplate();

            Func<Task> action = () => Task.Run(() => _serviceManager.MinIoDocumentService.UploadFiles(formTemplate.Id.ToString(), new List<IFormFile> { null }));
            var exception = await Record.ExceptionAsync(action);

            Assert.IsType<NullOrEmptyException>(exception);
        }

        [Fact]
        public async void UploadFiles_WhenCalledWithANullFileList_ANullOrEmptyListExceptionShouldBeThrown()
        {
            var formTemplate = _sharedFixture.CreateFormTemplate();

            Func<Task> action = () => Task.Run(() => _serviceManager.MinIoDocumentService.UploadFiles(formTemplate.Id.ToString(), null));
            var exception = await Record.ExceptionAsync(action);

            Assert.IsType<NullOrEmptyException>(exception);
        }

        [Fact]
        public async void UploadFiles_WhenCalledWithValidParameters_ADocumentIdShouldBeReturned()
        {
            var formTemplate = _sharedFixture.CreateFormTemplate();

            const string filePath = "C:\\Users\\Craig\\Downloads\\seuss.pdf";
            var fileName = filePath.Split(@"\").Last();
            
            await using var stream = new MemoryStream((await File.ReadAllBytesAsync(filePath)).ToArray());
            
            var formFile = new FormFile(stream, 0, stream.Length, "streamFile", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/pdf"
            };

            var documentIds = await _serviceManager.MinIoDocumentService.UploadFiles(formTemplate.Id.ToString(), new List<IFormFile> { formFile });

            Assert.True(documentIds.Count == 1);
        }
    }
}