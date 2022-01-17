using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using SimpleFormsService.Domain.Entities.Base;

namespace SimpleFormsService.Domain.Entities.Supporting.JSON;

[NotMapped]
public class FormSubmissionData : JsonEntityBase
{
    public FormSubmissionData(
        Identifier identifier,
        string templateId,
        string dateSubmitted,
        string submissionStatus,
        string createDate,
        string createUser,
        string updateDate,
        string updateUser,
        NotifyEmailAddress[] confirmationEmailAddresses,
        FormItem[] formItems,
        DocumentReference[] documentReferences) : base(createDate, createUser, updateDate, updateUser)
    {
        Identifier = identifier;
        TemplateId = templateId;
        DateSubmitted = dateSubmitted;
        SubmissionStatus = submissionStatus;
        ConfirmationEmailAddresses = confirmationEmailAddresses;
        FormItems = formItems;
        DocumentReferences = documentReferences;
    }

    [JsonPropertyOrder(1)]
    public Identifier Identifier { get; set; }
    [JsonPropertyOrder(2)]
    public string TemplateId { get; set; }
    [JsonPropertyOrder(3)]
    public string DateSubmitted { get; set; }
    [JsonPropertyOrder(4)]
    public string SubmissionStatus { get; set; }
    [JsonPropertyOrder(5)]
    public NotifyEmailAddress[] ConfirmationEmailAddresses { get; set; }
    [JsonPropertyOrder(6)]
    public FormItem[] FormItems { get; set; }
    [JsonPropertyOrder(7)]
    public DocumentReference[]? DocumentReferences { get; set; }
}