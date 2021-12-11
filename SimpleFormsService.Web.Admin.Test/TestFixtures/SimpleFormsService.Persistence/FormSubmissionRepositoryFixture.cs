using Microsoft.Extensions.DependencyInjection;
using SimpleFormsService.Domain.Repositories;
using SimpleFormsService.Persistence;
using SimpleFormsService.Web.Admin.Test.SharedFixtures;
using Xunit;

namespace SimpleFormsService.Web.Admin.Test.TestFixtures.SimpleFormsService.Persistence
{
    [Collection(Constants.ConcreteDatabaseTestCollectionName)]
    public class
        FormSubmissionRepositoryFixture : IClassFixture<ConcreteDatabaseSharedFixture<SimpleFormsServiceDbContext>>
    {
        private readonly ConcreteDatabaseSharedFixture<SimpleFormsServiceDbContext> _sharedFixture;
        private readonly IRepositoryManager _repositoryManager;

        public FormSubmissionRepositoryFixture(ConcreteDatabaseSharedFixture<SimpleFormsServiceDbContext> sharedFixture)
        {
            _sharedFixture = sharedFixture;
            _repositoryManager = sharedFixture.Container.GetService<IRepositoryManager>()!;
        }

        #region Create

        [Fact]
        public void Create_WhenCalledWithAValidFormSubmission_AFormSubmissionShouldBePersistedToTheDatabase()
        {
            var id = _sharedFixture.CreateFormSubmission();

            var formSubmission = _repositoryManager.FormSubmissionRepository.FindByIdAsync(id);

            Assert.NotNull(formSubmission.Result);
        }

        #endregion

        #region GetFormSubmissionByIdAsync

        [Fact]
        public void GetFormSubmissionByIdAsync_WhenCalledWithAnNonExistentId_NullShouldBeReturned()
        {
            const string id = "non existent id";

            var formSubmission = _repositoryManager.FormSubmissionRepository.GetFormSubmissionByIdAsync(id);

            Assert.Null(formSubmission.Result);
        }

        [Fact]
        public void GetFormSubmissionByIdAsync_WhenCalledWithAValidId_AFormSubmissionShouldBeReturned()
        {
            var id = _sharedFixture.CreateFormSubmission();

            var formSubmission = _repositoryManager.FormSubmissionRepository.GetFormSubmissionByIdAsync(id.ToString()).Result;

            Assert.Equal(id, formSubmission.Id);
        }

        #endregion
    }
}