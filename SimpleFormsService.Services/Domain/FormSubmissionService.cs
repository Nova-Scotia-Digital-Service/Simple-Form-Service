using Microsoft.AspNetCore.Http;
using SimpleFormsService.Configuration;
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
    private readonly INotificationService _gcNotificationService;

    public FormSubmissionService(IRepositoryManager repositoryManager, IDocumentService minioDocumentService, INotificationService gcNotificationService)
    {
        _repositoryManager = repositoryManager;
        _minioDocumentService = minioDocumentService;
        _gcNotificationService = gcNotificationService;
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

        _repositoryManager.FormSubmissionRepository.ClearTrackedEntities(); // todo remove after moving from service implementation to api implementation post mvp

        var formSubmission = _repositoryManager.FormSubmissionRepository.FindByCondition(x => x.Id == Guid.Parse(submissionId) && x.TemplateId == Guid.Parse(templateId)).FirstOrDefault();

        Guard.AgainstObjectNotFound(formSubmission, "form submission", submissionId, nameof(submissionId));

        var documentIds = await _minioDocumentService.UploadFiles(templateId, new List<IFormFile>{file}, cancellationToken);

        await UpdateFormSubmissionDocumentReferenceDataOnUpload(templateId, file, cancellationToken, documentIds, formSubmission);

        return formSubmission;
    }

    public async Task<FormSubmission> DeleteFile(string templateId, string submissionId, string documentId, CancellationToken cancellationToken = default)
    {
        Guard.AgainstNullEmptyOrWhiteSpace(templateId, nameof(templateId));
        Guard.AgainstInvalidGuidFormat(templateId, nameof(templateId));
        Guard.AgainstNullEmptyOrWhiteSpace(submissionId, nameof(submissionId));
        Guard.AgainstInvalidGuidFormat(submissionId, nameof(submissionId));
        Guard.AgainstNullEmptyOrWhiteSpace(documentId, nameof(documentId));
        Guard.AgainstInvalidGuidFormat(documentId, nameof(documentId));

        _repositoryManager.FormSubmissionRepository.ClearTrackedEntities();  // todo remove after moving from service implementation to api implementation post mvp 

        var formSubmission = _repositoryManager.FormSubmissionRepository.FindByCondition(x => x.Id == Guid.Parse(submissionId) && x.TemplateId == Guid.Parse(templateId)).FirstOrDefault();

        Guard.AgainstObjectNotFound(formSubmission, "form submission", submissionId, nameof(submissionId));

        var success = await _minioDocumentService.RemoveFile(templateId, documentId, cancellationToken);

        await UpdateFormSubmissionDocumentReferenceDataOnDelete(documentId, cancellationToken, success, formSubmission);

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
            
        _repositoryManager.FormSubmissionRepository.ClearTrackedEntities(); // todo remove after moving from service implementation to api implementation post mvp

        var formSubmission = _repositoryManager.FormSubmissionRepository.FindByCondition(x => x.Id == Guid.Parse(submissionId) && x.TemplateId == Guid.Parse(templateId)).FirstOrDefault();

        Guard.AgainstObjectNotFound(formSubmission, "form submission", submissionId, nameof(submissionId));

        UpdateFormSubmissionData(data, formSubmission);

        _repositoryManager.FormSubmissionRepository.Update(formSubmission);

        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

        handleNotifications();

        return formSubmission;

        void handleNotifications()
        {
            var notifyEmailAddresses = formSubmission.Data?.ConfirmationEmailAddresses;
            var emailAddresses = notifyEmailAddresses.Select(emailAddress => emailAddress.EmailAddress).ToList();
            _gcNotificationService.SendNotification(OpenshiftConfig.GCNotify_User_TemplateId, templateId, submissionId, emailAddresses);

            var formTemplate = _repositoryManager.FormTemplateRepository.FindByCondition(x => x.Id == Guid.Parse(templateId)).SingleOrDefault();
            var adminNotifyEmailAddresses = formTemplate?.Data?.AdminNotifyEmailAddresses;
            emailAddresses = adminNotifyEmailAddresses.Select(emailAddress => emailAddress.EmailAddress).ToList();
            _gcNotificationService.SendNotification(OpenshiftConfig.GCNotify_Admin_TemplateId, templateId, submissionId, emailAddresses);
        }
    }
    
    public async Task<FormSubmission> GetFormSubmissionByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        Guard.AgainstNullEmptyOrWhiteSpace(id, nameof(id));
        Guard.AgainstInvalidGuidFormat(id, nameof(id));

        var formSubmission = await _repositoryManager.FormSubmissionRepository.GetFormSubmissionByIdAsync(id, cancellationToken);

        Guard.AgainstObjectNotFound(formSubmission, "form submission", id, nameof(id));

        return formSubmission;
    }

    public async Task<FormSubmission> GetFormSubmissionByIdTemplateIdAsync(string id, string templateId, CancellationToken cancellationToken = default)
    {
        Guard.AgainstNullEmptyOrWhiteSpace(id, nameof(id));
        Guard.AgainstInvalidGuidFormat(id, nameof(id));
        Guard.AgainstNullEmptyOrWhiteSpace(templateId, nameof(templateId));
        Guard.AgainstInvalidGuidFormat(templateId, nameof(templateId));
        
        var formSubmission = await _repositoryManager.FormSubmissionRepository.GetFormSubmissionByIdTemplateIdAsync(id, templateId, cancellationToken);

        Guard.AgainstObjectNotFound(formSubmission, "form submission", id, nameof(id));

        return formSubmission;
    }


    public async Task<List<FormSubmission>> GetFormSubmissionsByTemplateIdAsync(string templateId, CancellationToken cancellationToken = default)
    {
        Guard.AgainstNullEmptyOrWhiteSpace(templateId, nameof(templateId));
        Guard.AgainstInvalidGuidFormat(templateId, nameof(templateId));

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

    private async Task UpdateFormSubmissionDocumentReferenceDataOnUpload(string templateId, IFormFile file, CancellationToken cancellationToken, List<string> uploadedDocumentIds, FormSubmission formSubmission)
    {
        if (uploadedDocumentIds.Count == 1)
        {
            var documentReferences = formSubmission.Data?.DocumentReferences?.ToList() ?? new List<DocumentReference>();
            documentReferences.Add(new DocumentReference(templateId, uploadedDocumentIds[0], file.FileName));

            if (formSubmission.Data != null)
                formSubmission.Data.DocumentReferences = documentReferences.ToArray();

            _repositoryManager.FormSubmissionRepository.Update(formSubmission);
            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }

    private async Task UpdateFormSubmissionDocumentReferenceDataOnDelete(string documentId, CancellationToken cancellationToken, bool success, FormSubmission formSubmission)
    {
        if (success)
        {
            var documentReferences = formSubmission.Data?.DocumentReferences.ToList() ?? new List<DocumentReference>();

            if (documentReferences.Count > 0)
            {
                var documentReferenceToRemove = documentReferences.FirstOrDefault(x => x.DocumentId == documentId);
                documentReferences.Remove(documentReferenceToRemove);

                if (formSubmission.Data != null)
                    formSubmission.Data.DocumentReferences = documentReferences.ToArray();

                _repositoryManager.FormSubmissionRepository.Update(formSubmission);
                await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
            }
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