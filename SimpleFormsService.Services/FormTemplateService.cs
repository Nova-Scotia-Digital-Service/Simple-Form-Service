using SimpleFormsService.Contract.Dtos.FormTemplate;
using SimpleFormsService.Domain.Entities.FormTemplate;
using SimpleFormsService.Domain.Exceptions;
using SimpleFormsService.Domain.Repositories;
using SimpleFormsService.Services.Abstractions;

namespace SimpleFormsService.Services
{
    internal sealed class FormTemplateService : ServiceBase, IFormTemplateService
    {
        private readonly IRepositoryManager _repositoryManager;

        public FormTemplateService(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;

        public async Task<FormTemplateDto> GetFormTemplateByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            Guard.AgainstNullEmptyOrWhiteSpace(id, nameof(id));

            var formTemplate = await _repositoryManager.FormTemplateRepository.GetFormTemplateByIdAsync(id, cancellationToken);

            Guard.AgainstObjectNotFound(formTemplate, "form template", id, nameof(id));

            var formTemplateDto = mapFormTemplateToFormTemplateDto(formTemplate);

            return formTemplateDto;
        }

        #region Private Helpers

        private static FormTemplateDto mapFormTemplateToFormTemplateDto(FormTemplate formTemplate)
        {
            var formTemplateDto = new FormTemplateDto
            {
               Id = formTemplate.Id.ToString(),
            };

            return formTemplateDto;
        }

        #endregion
    }
}