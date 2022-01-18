using Notify.Models.Responses;
using SimpleFormsService.Domain.Entities;

namespace SimpleFormsService.Services.Abstractions.Application
{
    public interface INotificationService
    {
        EmailNotificationResponse SendConfirmationNotification(FormTemplate formTemplate, FormSubmission formSubmission, List<string> emailAddresses);
        EmailNotificationResponse SendAdminNotification(FormTemplate formTemplate, FormSubmission formSubmission, List<string> emailAddresses);
    }
}
