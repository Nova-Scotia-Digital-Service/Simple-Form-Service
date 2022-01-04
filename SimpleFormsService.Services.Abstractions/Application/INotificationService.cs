using Notify.Client;
using Notify.Models.Responses;

namespace SimpleFormsService.Services.Abstractions.Application
{
    public interface INotificationService
    {
        EmailNotificationResponse SendNotification(NotificationClient client, string templateId, string formId, string submissionID, List<string> emailAddresses);
    }
}
