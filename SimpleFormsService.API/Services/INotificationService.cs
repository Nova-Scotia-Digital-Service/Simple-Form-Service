using Notify.Client;
using Notify.Models.Responses;

namespace SimpleFormsService.API.Services
{
    public interface INotificationService
    {
        EmailNotificationResponse sendNotification(NotificationClient client, string templateId, string formId, string submissionID);
    }
}
