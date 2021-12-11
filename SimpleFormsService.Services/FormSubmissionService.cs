using System.Text.Json;
using SimpleFormsService.Contract.Dtos.FormSubmission;
using SimpleFormsService.Domain.Entities.FormSubmission;
using SimpleFormsService.Domain.Exceptions;
using SimpleFormsService.Domain.Repositories;
using SimpleFormsService.Services.Abstractions;

namespace SimpleFormsService.Services
{
    internal sealed class FormSubmissionService : ServiceBase, IFormSubmissionService
    {
        private readonly IRepositoryManager _repositoryManager;

        public FormSubmissionService(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;
        
        public async Task<FormSubmissionDto> GetFormSubmissionByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            Guard.AgainstNullEmptyOrWhiteSpace(id, nameof(id));

            var formSubmission = await _repositoryManager.FormSubmissionRepository.GetFormSubmissionByIdAsync(id, cancellationToken);

            Guard.AgainstObjectNotFound(formSubmission, "form submission", id, nameof(id));

            var formSubmissionDto = mapFormSubmissionToFormSubmissionDto(formSubmission);

            return formSubmissionDto;
        }

        public async Task<List<FormSubmissionDto>> GetFormSubmissionsByTemplateIdAsync(string templateId, CancellationToken cancellationToken = default)
        {
            Guard.AgainstNullEmptyOrWhiteSpace(templateId, nameof(templateId));

            var formSubmissions = await _repositoryManager.FormSubmissionRepository.GetFormSubmissionsByTemplateIdAsync(templateId, cancellationToken);

            Guard.AgainstEmptyList(formSubmissions, "form submissions", templateId, nameof(templateId));

            var formSubmissionDtos = mapFormSubmissionsToFormSubmissionDtos(formSubmissions);

            return formSubmissionDtos;
        }

        #region Private Helpers

        private static FormSubmissionDto mapFormSubmissionToFormSubmissionDto(FormSubmission formSubmission)
        {
            var formSubmissionDto = new FormSubmissionDto
            {
                Id = formSubmission.Id.ToString(),
                TemplateId = formSubmission.TemplateId.ToString(),
                SubmissionData = JsonSerializer.Serialize(formSubmission.SubmissionData)
            };

            return formSubmissionDto;
        }

        private static List<FormSubmissionDto> mapFormSubmissionsToFormSubmissionDtos(List<FormSubmission> assessments)
        {
            var formSubmissionDtos = new List<FormSubmissionDto>();

            foreach (var assessment in assessments)
            {
                formSubmissionDtos.Add(mapFormSubmissionToFormSubmissionDto(assessment));
            }

            return formSubmissionDtos;
        }

        #endregion
    }
}