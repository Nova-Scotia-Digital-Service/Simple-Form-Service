using Notify.Models.Responses;

namespace SimpleFormsService.Services.Abstractions.Application
{
    public interface INotificationService
    {
        EmailNotificationResponse SendNotification(string gcNotifyTemplateId, string formTemplateId, string formSubmissionId, List<string> emailAddresses);
    }
}
