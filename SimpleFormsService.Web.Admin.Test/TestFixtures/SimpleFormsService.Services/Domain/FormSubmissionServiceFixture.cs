using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleFormsService.Domain;
using SimpleFormsService.Domain.Entities.Supporting;
using SimpleFormsService.Domain.Exceptions;
using SimpleFormsService.Persistence;
using SimpleFormsService.Services.Abstractions;
using SimpleFormsService.Test.SharedFixtures;
using Xunit;

namespace SimpleFormsService.Test.TestFixtures.SimpleFormsService.Services.Domain
{
    [Collection(Constants.ConcreteDatabaseTestCollectionName)]
    public class FormSubmissionServiceFixture : IClassFixture<ConcreteDatabaseSharedFixture<SimpleFormsServiceDbContext>>
    {
        private readonly ConcreteDatabaseSharedFixture<SimpleFormsServiceDbContext> _sharedFixture;
        private readonly IServiceManager _serviceManager;

        public FormSubmissionServiceFixture(ConcreteDatabaseSharedFixture<SimpleFormsServiceDbContext> sharedFixture)
        {
            _sharedFixture = sharedFixture;
            _serviceManager = sharedFixture.Container.GetService<IServiceManager>()!;
        }

        #region Init

        [Fact]
        public void Init_WhenCalledWithAValidTemplateId_AFormSubmissionShouldBeReturned()
        {
            var formTemplate = _sharedFixture.CreateFormTemplate();
            var templateId = formTemplate.Id.ToString();

            var formSubmission = _serviceManager.FormSubmissionService.Init(templateId).Result;

            Assert.Equal(templateId, formSubmission.TemplateId.ToString());
        }

        [Fact]
        public async void Init_WhenCalledWithAnInvalidTemplateId_AnInvalidFormatExceptionShouldBeThrown()
        {
            var templateId = "invalid template id";

            Func<Task> action = () => Task.Run(() => _serviceManager.FormSubmissionService.Init(templateId));
            var exception = await Record.ExceptionAsync(action);

            Assert.IsType<InvalidFormatException>(exception);
        }

        [Fact]
        public async void Init_WhenCalledWithANonExistentTemplateId_AnObjectNotFoundException()
        {
            var templateId = Guid.NewGuid().ToString();

            Func<Task> action = () => Task.Run(() => _serviceManager.FormSubmissionService.Init(templateId));
            var exception = await Record.ExceptionAsync(action);

            Assert.IsType<DbUpdateException>(exception);
        }

        #endregion

        #region UploadFile

        [Fact]
        public void UploadFile_WhenCalledWithValidParameters_AFileShouldBeUploadedAndAFormSubmissionWithADocumentReferenceShouldBeReturned()
        {
            var formTemplate = _sharedFixture.CreateFormTemplate();
            var templateId = formTemplate.Id.ToString();
            var formSubmission = _sharedFixture.CreateFormSubmission(formTemplate);
            var submissionId = formSubmission.Id.ToString();

            const string filePath = "C:\\Users\\Craig\\Downloads\\seuss.pdf";
            var fileName = filePath.Split(@"\").Last();

            using var stream = new MemoryStream((File.ReadAllBytes(filePath)).ToArray());

            var formFile = new FormFile(stream, 0, stream.Length, "streamFile", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/pdf"
            };
            
            formSubmission = _serviceManager.FormSubmissionService.UploadFile(templateId, submissionId, formFile).Result;
            
            Assert.Equal(formFile.FileName, formSubmission.Data.DocumentReferences[2].Filename);
        }

        #endregion

        #region SubmitForm

        [Fact]
        public void SubmitForm_WhenCalledWithValidParameters_AFormSubmissionShouldBeUpdated()
        {
            var formTemplate = _sharedFixture.CreateFormTemplate();
            var templateId = formTemplate.Id.ToString();
            var formSubmission = _sharedFixture.CreateFormSubmission(formTemplate);
            var submissionId = formSubmission.Id.ToString();
            
            formSubmission = _serviceManager.FormSubmissionService.SubmitForm(templateId, submissionId, formSubmission.Data).Result;

            Assert.True(formSubmission.Data.SubmissionStatus == FormSubmissionStatus.Submitted.GetEnumMemberAttributeValueFromEnumValue());
        }

        #endregion

        #region GetFormSubmissionByIdAsync

        [Fact]
        public async void GetFormSubmissionByIdAsync_WhenCalledWithEmptyId_NullEmptyOrWhitespaceExceptionShouldBeReturned()
        {
            Func<Task> action = () => Task.Run(() => _serviceManager.FormSubmissionService.GetFormSubmissionByIdAsync(string.Empty));
            var exception = await Record.ExceptionAsync(action);

            Assert.IsType<NullEmptyOrWhitespaceException>(exception);
        }

        [Fact]
        public async void GetFormSubmissionByIdAsync_WhenCalledWithANonExistentId_ObjectNotFoundExceptionShouldBeReturned()
        {
            var id = "non existent id";

            Func<Task> action = () => Task.Run(() => _serviceManager.FormSubmissionService.GetFormSubmissionByIdAsync(id));
            var exception = await Record.ExceptionAsync(action);

            Assert.IsType<ObjectNotFoundException>(exception);
        }

        [Fact]
        public void GetFormSubmissionByIdAsync_WhenCalledWithAValidFormSubmission_AFormSubmissionShouldBeReturned()
        {
            var formTemplate = _sharedFixture.CreateFormTemplate();
            var formSubmission = _sharedFixture.CreateFormSubmission(formTemplate);
            var submissionId = formSubmission.Id.ToString();

            formSubmission = _serviceManager.FormSubmissionService.GetFormSubmissionByIdAsync(submissionId).Result;

            Assert.Equal(submissionId, formSubmission.Id.ToString());
        }

        #endregion
    }
}