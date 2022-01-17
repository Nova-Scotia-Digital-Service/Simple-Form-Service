using SimpleFormsService.Domain.Entities;
using SimpleFormsService.Domain.Exceptions;
using SimpleFormsService.Domain.Repositories;
using SimpleFormsService.Services.Abstractions.Application;
using SimpleFormsService.Services.Abstractions.Domain;

namespace SimpleFormsService.Services.Domain
{
    internal sealed class FormTemplateService : ServiceBase, IFormTemplateService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IFormTemplateSecurityService _formTemplateSecurityService;

        public FormTemplateService(IRepositoryManager repositoryManager, IFormTemplateSecurityService formTemplateSecurityService)
        {
            _repositoryManager = repositoryManager;
            _formTemplateSecurityService = formTemplateSecurityService;
        }

        public async Task<FormTemplate> GetFormTemplateByIdAsync(string templateId, CancellationToken cancellationToken = default)
        {
            Guard.AgainstNullEmptyOrWhiteSpace(templateId, nameof(templateId));
            Guard.AgainstInvalidGuidFormat(templateId, nameof(templateId));

            // todo move this to an attribute that uses service locator to get access to the form template security service to genericise it accross this service
            var hasAccess =  await _formTemplateSecurityService.HasAccess(templateId, cancellationToken);

            if (hasAccess)
            {
                var formTemplate = await _repositoryManager.FormTemplateRepository.GetFormTemplateByIdAsync(templateId, cancellationToken);

                Guard.AgainstObjectNotFound(formTemplate, "form template", templateId, nameof(templateId));

                return formTemplate;
            }

            throw new NotAuthorizedException("form template", templateId);
        }
    }
}