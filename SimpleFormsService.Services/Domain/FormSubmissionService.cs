using Microsoft.AspNetCore.Http;
using SimpleFormsService.Domain;
using SimpleFormsService.Domain.Entities;
using SimpleFormsService.Domain.Entities.Supporting;
using SimpleFormsService.Domain.Entities.Supporting.JSON;
using SimpleFormsService.Domain.Exceptions;
using SimpleFormsService.Domain.Repositories;
using SimpleFormsService.Services.Abstractions.Application;
using SimpleFormsService.Services.Abstractions.Domain;

namespace SimpleFormsService.Services.Domain;

internal sealed class FormSubmissionService : ServiceBase, IFormSubmissionService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IDocumentService _minioDocumentService;

    public FormSubmissionService(IRepositoryManager repositoryManager, IDocumentService minioDocumentService)
    {
        _repositoryManager = repositoryManager;
        _minioDocumentService = minioDocumentService;
    }

    public async Task<FormSubmission> Init(string templateId, CancellationToken cancellationToken = default)
    {
        Guard.AgainstNullEmptyOrWhiteSpace(templateId, nameof(templateId));
        Guard.AgainstInvalidGuidFormat(templateId, nameof(templateId));

        var formSubmission = InitializeFormSubmission(templateId);

        _repositoryManager.FormSubmissionRepository.Create(formSubmission);

        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

        return formSubmission;
    }

    public async Task<FormSubmission> UploadFile(string templateId, string submissionId, IFormFile file, CancellationToken cancellationToken = default)
    {
        Guard.AgainstNullEmptyOrWhiteSpace(templateId, nameof(templateId));
        Guard.AgainstInvalidGuidFormat(templateId, nameof(templateId));
        Guard.AgainstNullEmptyOrWhiteSpace(submissionId, nameof(submissionId));
        Guard.AgainstInvalidGuidFormat(submissionId, nameof(submissionId));
        Guard.AgainstNullOrEmptyObject(file, nameof(file));

        _repositoryManager.FormSubmissionRepository.ClearTrackedEntities();

        var formSubmission = _repositoryManager.FormSubmissionRepository.FindByCondition(x => x.Id == Guid.Parse(submissionId) && x.TemplateId == Guid.Parse(templateId)).FirstOrDefault();

        Guard.AgainstObjectNotFound(formSubmission, "form submission", submissionId, nameof(submissionId));

        var documentIds = await _minioDocumentService.UploadFiles(new List<IFormFile>{file}, templateId, cancellationToken);

        await UpdateFormSubmissionDocumentReferenceData(templateId, file, cancellationToken, documentIds, formSubmission);

        return formSubmission;
    }
        
    public async Task<FormSubmission> SubmitForm(string templateId, string submissionId, FormSubmissionData data, CancellationToken cancellationToken = default)
    {
        Guard.AgainstNullEmptyOrWhiteSpace(templateId, nameof(templateId));
        Guard.AgainstInvalidGuidFormat(templateId, nameof(templateId));
        Guard.AgainstNullEmptyOrWhiteSpace(submissionId, nameof(submissionId));
        Guard.AgainstInvalidGuidFormat(submissionId, nameof(submissionId));
        Guard.AgainstNullOrEmptyObject(data, nameof(data));
        Guard.AgainstNullOrEmptyObject(data.FormItems, nameof(data.FormItems));
            
        _repositoryManager.FormSubmissionRepository.ClearTrackedEntities(); 

        var formSubmission = _repositoryManager.FormSubmissionRepository.FindByCondition(x => x.Id == Guid.Parse(submissionId) && x.TemplateId == Guid.Parse(templateId)).FirstOrDefault();

        Guard.AgainstObjectNotFound(formSubmission, "form submission", submissionId, nameof(submissionId));

        UpdateFormSubmissionData(data, formSubmission);

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

    #region Private Helpers

    private static FormSubmission InitializeFormSubmission(string templateId)
    {
        var submissionId = Guid.NewGuid();

        var friendlyName = submissionId.ToString().Substring(0, submissionId.ToString().IndexOf("-", StringComparison.Ordinal));

        var identifier = new Identifier(submissionId.ToString(), friendlyName);

        var data = new FormSubmissionData(
            identifier,
            templateId,
            null,
            FormSubmissionStatus.Initialized.GetEnumMemberAttributeValueFromEnumValue(),
            SystemTime.NowString,
            Constants.DefaultUser,
            null,
            null,
            null,
            null,
            null);

        var formSubmission = new FormSubmission(submissionId, Guid.Parse(templateId), data);

        return formSubmission;
    }

    private async Task UpdateFormSubmissionDocumentReferenceData(string templateId, IFormFile file, CancellationToken cancellationToken, List<string> uploadedDocumentIds, FormSubmission formSubmission)
    {
        if (uploadedDocumentIds.Count == 1)
        {
            var documentReferences = formSubmission.Data?.DocumentReferences.ToList() ?? new List<DocumentReference>();
            documentReferences.Add(new DocumentReference(templateId, uploadedDocumentIds[0], file.FileName));

            if (formSubmission.Data != null)
                formSubmission.Data.DocumentReferences = documentReferences.ToArray();

            _repositoryManager.FormSubmissionRepository.Update(formSubmission);
            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }

    private static void UpdateFormSubmissionData(FormSubmissionData data, FormSubmission formSubmission)
    {
        if (formSubmission.Data != null)
        {
            data.Identifier = formSubmission.Data.Identifier;
            data.TemplateId = formSubmission.Data.TemplateId;
            data.DateSubmitted = SystemTime.NowString;
            data.SubmissionStatus = FormSubmissionStatus.Submitted.GetEnumMemberAttributeValueFromEnumValue();
            data.CreateUser = formSubmission.Data.CreateUser;
            data.CreateDate = formSubmission.Data.CreateDate;
            data.UpdateUser = Constants.DefaultUser; // todo read user principal and make a proper determination as to who the user is?
            data.UpdateDate = SystemTime.NowString;
        }

        formSubmission.Data = data;
    }

    #endregion
}