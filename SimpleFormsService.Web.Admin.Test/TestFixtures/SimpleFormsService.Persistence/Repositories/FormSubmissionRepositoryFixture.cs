using Microsoft.Extensions.DependencyInjection;
using SimpleFormsService.Domain.Repositories;
using SimpleFormsService.Persistence;
using SimpleFormsService.Test.SharedFixtures;
using Xunit;

namespace SimpleFormsService.Test.TestFixtures.SimpleFormsService.Persistence.Repositories
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
            var formTemplate = _sharedFixture.CreateFormTemplate();
            var formSubmission = _sharedFixture.CreateFormSubmission(formTemplate);

            formSubmission = _repositoryManager.FormSubmissionRepository.FindByIdAsync(formSubmission.Id).Result;

            Assert.NotNull(formSubmission);
        }

        #endregion

        #region GetFormSubmissionByIdAsync

        [Fact]
        public void GetFormSubmissionByIdAsync_WhenCalledWithAnNonExistentId_NullShouldBeReturned()
        {
            const string id = "non existent id";

            var formSubmission = _repositoryManager.FormSubmissionRepository.GetFormSubmissionByIdAsync(id).Result;

            Assert.Null(formSubmission);
        }

        [Fact]
        public void GetFormSubmissionByIdAsync_WhenCalledWithAValidId_AFormSubmissionShouldBeReturned()
        {
            var formTemplate = _sharedFixture.CreateFormTemplate();
            var formSubmission = _sharedFixture.CreateFormSubmission(formTemplate);
            var submissionId = formSubmission.Id.ToString();

            formSubmission = _repositoryManager.FormSubmissionRepository.GetFormSubmissionByIdAsync(submissionId).Result;

            Assert.Equal(submissionId, formSubmission.Id.ToString());
        }

        #endregion

    }
}