using Microsoft.EntityFrameworkCore;
using SimpleFormsService.Domain.Entities;
using SimpleFormsService.Domain.Exceptions;
using SimpleFormsService.Domain.Repositories;
using SimpleFormsService.Services.Abstractions.Domain;

namespace SimpleFormsService.Services.Domain
{
    internal sealed class FormTemplateService : ServiceBase, IFormTemplateService
    {
        private readonly IRepositoryManager _repositoryManager;

        public FormTemplateService(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;

        public async Task<FormTemplate> GetFormTemplateByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            Guard.AgainstNullEmptyOrWhiteSpace(id, nameof(id));

            var formTemplate = await _repositoryManager.FormTemplateRepository.GetFormTemplateByIdAsync(id, cancellationToken);

            Guard.AgainstObjectNotFound(formTemplate, "form template", id, nameof(id));

            return formTemplate;
        }
    }
}