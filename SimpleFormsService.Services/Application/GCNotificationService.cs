using Notify.Client;
using Notify.Models.Responses;
using SimpleFormsService.Configuration;
using SimpleFormsService.Services.Abstractions.Application;

namespace SimpleFormsService.Services.Application
{
    public class GCNotificationService : ServiceBase ,INotificationService
    {
        private readonly string reference = "Simple form service email notification";

        /// <summary>
        /// Send email notification to Admin with a URL to the form.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="templateId"></param>
        /// <param name="formId"></param>
        /// <param name="submissionId"></param>
        /// <param name="emailAddresses"></param>
        /// <returns></returns>
        public EmailNotificationResponse SendNotification(NotificationClient client, string templateId, string formId, string submissionId, List<string> emailAddresses) // TODO inject NotificationClient
        {
            client = new NotificationClient(OpenshiftConfig.GCNotify_BaseURL, OpenshiftConfig.GCNotify_ApiKey);
            var response = new EmailNotificationResponse();
            var successCount = 0;
            var failedCount = 0;

            var personalisation = new Dictionary<string, dynamic>
            {
                {"url", "https://www.test.com"}
            };
            if (emailAddresses != null && emailAddresses.Count > 0)
            {
                Console.WriteLine("===== INFO: Ready to send email to Admin... Form ID: " + formId + " Submission Id: " + submissionId);
                foreach (var email in emailAddresses)
                {
                    try
                    {
                        response = client.SendEmail(email, templateId, personalisation, reference);
                        successCount++;
                    }
                    catch
                    {
                        Console.WriteLine("====== ERROR: Unable to send email to " + email + " ========");
                        failedCount++;
                    }
                }
            }
            Console.WriteLine("====== INFO: Total failed count equals to" + failedCount + "; Success count equals to " + successCount + " ========");
            return response;
        }
    }
}
