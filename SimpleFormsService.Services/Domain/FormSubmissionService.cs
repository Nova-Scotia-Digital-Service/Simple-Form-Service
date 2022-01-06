using SimpleFormsService.Domain.Entities;
using SimpleFormsService.Domain.Entities.Supporting.JSON;
using SimpleFormsService.Domain.Exceptions;
using SimpleFormsService.Domain.Repositories;
using SimpleFormsService.Services.Abstractions.Domain;

namespace SimpleFormsService.Services.Domain
{
    internal sealed class FormSubmissionService : ServiceBase, IFormSubmissionService
    {
        private readonly IRepositoryManager _repositoryManager;

        public FormSubmissionService(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;

        public async Task<FormSubmission> Init(string templateId, CancellationToken cancellationToken = default)
        {
            Guard.AgainstNullEmptyOrWhiteSpace(templateId, nameof(templateId));
            Guard.AgainstInvalidGuidFormat(templateId, nameof(templateId));

            var formSubmission = new FormSubmission(Guid.NewGuid(), Guid.Parse(templateId), null);

            _repositoryManager.FormSubmissionRepository.Create(formSubmission);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

            return formSubmission;
        }

        public async Task<FormSubmission> SubmitForm(string templateId, string submissionId, FormSubmissionData data, CancellationToken cancellationToken = default)
        {
            Guard.AgainstNullEmptyOrWhiteSpace(templateId, nameof(templateId));
            Guard.AgainstInvalidGuidFormat(templateId, nameof(templateId));
            Guard.AgainstNullEmptyOrWhiteSpace(submissionId, nameof(submissionId));
            Guard.AgainstInvalidGuidFormat(submissionId, nameof(submissionId));

            // todo add user create/update and date create/update validation if it is not going to be programatically set
            
            // todo ensure this fixes kevin's problem while consuming this service directly from the public app until this is called through a deployed api
            _repositoryManager.FormSubmissionRepository.ClearTrackedEntities();

            var formSubmission = _repositoryManager.FormSubmissionRepository.FindByCondition(x => x.Id == Guid.Parse(submissionId) && x.TemplateId == Guid.Parse(templateId))
                .FirstOrDefault();

            Guard.AgainstObjectNotFound(formSubmission, "form submission", submissionId, nameof(submissionId));

            // todo programatically set user create/update and date create/update fields if not passed from ui or implemented within simpleformsservicedbcontext  

            formSubmission.Data = data;

            _repositoryManager.FormSubmissionRepository.Update(formSubmission);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

            return formSubmission;
        }

        public async Task<FormSubmission> GetFormSubmissionByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            Guard.AgainstNullEmptyOrWhiteSpace(id, nameof(id));

            var formSubmission = await _repositoryManager.FormSubmissionRepository.GetFormSubmissionByIdAsync(id, cancellationToken);

            Guard.AgainstObjectNotFound(formSubmission, "form submission", id, nameof(id));

            return formSubmission;
        }

        public async Task<List<FormSubmission>> GetFormSubmissionsByTemplateIdAsync(string templateId, CancellationToken cancellationToken = default)
        {
            Guard.AgainstNullEmptyOrWhiteSpace(templateId, nameof(templateId));

            var formSubmissions = await _repositoryManager.FormSubmissionRepository.GetFormSubmissionsByTemplateIdAsync(templateId, cancellationToken);

            Guard.AgainstEmptyList(formSubmissions, "form submissions", templateId, nameof(templateId));

            return formSubmissions;
        }
    }
}