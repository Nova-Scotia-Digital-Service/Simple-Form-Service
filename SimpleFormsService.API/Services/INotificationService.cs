using Notify.Client;
using Notify.Models.Responses;

namespace SimpleFormsService.API.Services
{
    /// <summary>
    /// Email notification to Admin 
    /// </summary>
    public interface INotificationService
    {
        EmailNotificationResponse SendNotification(NotificationClient client, string templateId, string formId, string submissionID, List<string> emailAddresses);
    }
}
