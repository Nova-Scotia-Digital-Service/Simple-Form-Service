using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleFormsService.Domain.Entities.FormSubmission.Supporting.JSON;

[NotMapped]
public class FormSubmissionData
{
    public FormSubmissionData(string dateSubmitted, string submissionStatus, NotifyEmailAddress[] notifyEmailAddresses, FormItem[] formItems, DocumentReference[] documentReferences)
    {
        DateSubmitted = dateSubmitted;
        SubmissionStatus = submissionStatus;
        NotifyEmailAddresses = notifyEmailAddresses;
        FormItems = formItems;
        DocumentReferences = documentReferences;
    }
    public string DateSubmitted { get; set; }
    public string SubmissionStatus { get; set; }
    public NotifyEmailAddress[] NotifyEmailAddresses { get; set; }
    public FormItem[] FormItems { get; set; }
    public DocumentReference[]? DocumentReferences { get; set; }
}