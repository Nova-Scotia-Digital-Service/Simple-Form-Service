using Microsoft.Extensions.DependencyInjection;
using SimpleFormsService.Domain.Repositories;
using SimpleFormsService.Persistence;
using SimpleFormsService.Web.Admin.Test.SharedFixtures;
using Xunit;

namespace SimpleFormsService.Web.Admin.Test.TestFixtures.SimpleFormsService.Persistence
{
    [Collection(Constants.ConcreteDatabaseTestCollectionName)]
    public class FormTemplateRepositoryTestFixture : IClassFixture<ConcreteDatabaseSharedFixture<SimpleFormsServiceDbContext>>
    {
        private readonly ConcreteDatabaseSharedFixture<SimpleFormsServiceDbContext> _sharedFixture;
        private readonly IFormTemplateRepository _repository;

        public FormTemplateRepositoryTestFixture(ConcreteDatabaseSharedFixture<SimpleFormsServiceDbContext> sharedFixture)
        {
            _sharedFixture = sharedFixture;
            _repository = sharedFixture.Container.GetService<IFormTemplateRepository>()!;
        }

        #region GetFormTemplateByIdAsync

        [Fact]
        public void GetFormTemplateByIdAsync_WhenCalledWithAnNonExistentId_NullShouldBeReturned()
        {
            const string id = "non existent id";

            var formTemplate = _repository.GetFormTemplateByIdAsync(id);

            Assert.Null(formTemplate.Result);
        }

        [Fact]
        public void GetFormTemplateByIdAsync_WhenCalledWithAValidId_AFormTemplateShouldBeReturned()
        {
            var id = _sharedFixture.CreateFormTemplate();

            var formTemplate = _repository.GetFormTemplateByIdAsync(id.ToString());

            Assert.Equal(id, formTemplate.Result.Id);
        }

        #endregion
    }
}