using SimpleFormsService.Domain.Entities;
using SimpleFormsService.Domain.Entities.Supporting.JSON;
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
            var isUserAuthorized =  await _formTemplateSecurityService.IsUserAuthorized(templateId, cancellationToken);

            if (isUserAuthorized)
            {
                var formTemplate = await _repositoryManager.FormTemplateRepository.GetFormTemplateByIdAsync(templateId, cancellationToken);

                Guard.AgainstObjectNotFound(formTemplate, "form template", templateId, nameof(templateId));

                return formTemplate;
            }

            throw new NotAuthorizedException("form template", templateId);
        }

        public async Task<List<Identifier>> GetFormTemplatesAsync(CancellationToken cancellationToken = default)
        {
            var formTemplates = await _repositoryManager.FormTemplateRepository.GetFormTemplatesAsync(cancellationToken);

            Guard.AgainstObjectNotFound(formTemplates, "form templates");

            var formTemplateIdentifiers = new List<Identifier>(formTemplates.Count);
            formTemplateIdentifiers.AddRange(formTemplates.Select(formTemplate => formTemplate.Data.Identifier));

            return formTemplateIdentifiers;
        }
    }
}