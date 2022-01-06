﻿using SimpleFormsService.Domain.Entities;
using SimpleFormsService.Domain.Entities.Supporting.JSON;

namespace SimpleFormsService.Services.Abstractions.Domain
{
    public interface IFormSubmissionService
    {
        Task<FormSubmission> Init(string templateId, CancellationToken cancellationToken = default);
        Task<FormSubmission> SubmitForm(string templateId, string submissionId, FormSubmissionData data, CancellationToken cancellationToken = default);
        Task<FormSubmission> GetFormSubmissionByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<List<FormSubmission>> GetFormSubmissionsByTemplateIdAsync(string templateId, CancellationToken cancellationToken = default);
    }
}