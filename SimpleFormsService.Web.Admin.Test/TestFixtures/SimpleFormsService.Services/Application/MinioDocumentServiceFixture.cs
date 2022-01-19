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

        #region GetObject

        [Fact]
        public async void GetObject_WhenCalledWithValidParameters_ADocumentIdShouldBeReturned()
        {
            var documentIds = uploadAFileToMinio(out var formTemplateId);

            var fileStreamResultAdapter = await _serviceManager.MinIoDocumentService.GetObject(formTemplateId, documentIds.FirstOrDefault());

            Assert.True(fileStreamResultAdapter.MemoryStream != null);
        }

        #endregion

        #region UploadFiles

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
        public void UploadFiles_WhenCalledWithValidParameters_ADocumentIdShouldBeReturned()
        {
            var documentIds = uploadAFileToMinio(out var formTemplateId);

            Assert.True(documentIds.Count == 1);
        }

        #endregion

        #region Private Helpers

        public List<string> uploadAFileToMinio(out string formTemplateId)
        {
            var formTemplate = _sharedFixture.CreateFormTemplate();
            formTemplateId = formTemplate.Id.ToString();

            const string filePath = "C:\\Users\\Craig\\Downloads\\seuss.pdf";
            var fileName = filePath.Split(@"\").Last();

            using var stream = new MemoryStream((File.ReadAllBytesAsync(filePath).Result).ToArray());

            var formFile = new FormFile(stream, 0, stream.Length, "streamFile", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/pdf"
            };

            var documentIds = _serviceManager.MinIoDocumentService.UploadFiles(formTemplate.Id.ToString(), new List<IFormFile> {formFile}).Result;

            return documentIds;
        }

        #endregion
    }
}