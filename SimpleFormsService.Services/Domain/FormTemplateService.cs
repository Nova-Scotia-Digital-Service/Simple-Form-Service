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

        public async Task<bool> HasAccess(string templateId, string email, CancellationToken cancellationToken = default)
        {
            Guard.AgainstNullEmptyOrWhiteSpace(templateId, nameof(templateId));
            Guard.AgainstInvalidGuidFormat(templateId, nameof(templateId));
            Guard.AgainstNullEmptyOrWhiteSpace(email, nameof(email));

            var formTemplate = await _repositoryManager.FormTemplateRepository.FindByCondition(x => x.Id == Guid.Parse(templateId))
                .FirstOrDefaultAsync(cancellationToken);

            Guard.AgainstObjectNotFound(formTemplate, "form template", templateId, nameof(formTemplate));

            var authorizedUsers = formTemplate.Data.AuthorizedUsers;

            return authorizedUsers.Any(authorizedUser => email.Equals(authorizedUser.EmailAddress));
        }
    }
}