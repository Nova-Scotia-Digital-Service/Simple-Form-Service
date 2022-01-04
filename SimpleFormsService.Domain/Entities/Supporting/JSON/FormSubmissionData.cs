using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using SimpleFormsService.Domain.Entities.Base;

namespace SimpleFormsService.Domain.Entities.Supporting.JSON;

[NotMapped]
public class FormSubmissionData : JsonEntityBase
{
    public FormSubmissionData(
        string submissionId,
        string templateId,
        string dateSubmitted,
        string submissionStatus,
        string createDate,
        string createUser,
        string updateDate,
        string updateUser,
        NotifyEmailAddress[] notifyEmailAddresses,
        FormItem[] formItems,
        DocumentReference[] documentReferences) : base(createDate, createUser, updateDate, updateUser)
    {
        SubmissionId = submissionId;
        TemplateId = templateId;
        DateSubmitted = dateSubmitted;
        SubmissionStatus = submissionStatus;
        NotifyEmailAddresses = notifyEmailAddresses;
        FormItems = formItems;
        DocumentReferences = documentReferences;
    }

    [JsonPropertyOrder(1)]
    public string SubmissionId { get; set; }
    [JsonPropertyOrder(2)]
    public string TemplateId { get; set; }
    [JsonPropertyOrder(3)]
    public string DateSubmitted { get; set; }
    [JsonPropertyOrder(4)]
    public string SubmissionStatus { get; set; }
    [JsonPropertyOrder(5)]
    public NotifyEmailAddress[] NotifyEmailAddresses { get; set; }
    [JsonPropertyOrder(6)]
    public FormItem[] FormItems { get; set; }
    [JsonPropertyOrder(7)]
    public DocumentReference[]? DocumentReferences { get; set; }
}