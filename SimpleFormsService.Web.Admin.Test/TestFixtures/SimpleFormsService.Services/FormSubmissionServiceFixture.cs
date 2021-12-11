using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SimpleFormsService.Domain.Exceptions;
using SimpleFormsService.Persistence;
using SimpleFormsService.Services.Abstractions;
using SimpleFormsService.Web.Admin.Test.SharedFixtures;
using Xunit;

namespace SimpleFormsService.Web.Admin.Test.TestFixtures.SimpleFormsService.Services
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
            var id = _sharedFixture.CreateFormSubmission();

            var formSubmission = _serviceManager.FormSubmissionService.GetFormSubmissionByIdAsync(id.ToString()).Result;

            Assert.Equal(id.ToString(),formSubmission.Id);
        }

        #endregion
    }
}