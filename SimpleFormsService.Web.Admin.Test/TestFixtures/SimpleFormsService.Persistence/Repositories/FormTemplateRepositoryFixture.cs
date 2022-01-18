using Microsoft.Extensions.DependencyInjection;
using SimpleFormsService.Domain.Repositories;
using SimpleFormsService.Persistence;
using SimpleFormsService.Test.SharedFixtures;
using Xunit;

namespace SimpleFormsService.Test.TestFixtures.SimpleFormsService.Persistence.Repositories
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

            var formTemplate = _repository.GetFormTemplateByIdAsync(id).Result;

            Assert.Null(formTemplate);
        }

        [Fact]
        public void GetFormTemplateByIdAsync_WhenCalledWithAValidId_AFormTemplateShouldBeReturned()
        {
            var formTemplate = _sharedFixture.CreateFormTemplate();
            var templateId = formTemplate.Id.ToString();

            formTemplate = _repository.GetFormTemplateByIdAsync(templateId).Result;

            Assert.Equal(templateId, formTemplate.Id.ToString());
        }

        #endregion
    }
}