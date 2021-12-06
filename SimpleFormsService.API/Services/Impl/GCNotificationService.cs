using Notify.Client;
using Notify.Models.Responses;
using SimpleFormsService.API.Configs;

namespace SimpleFormsService.API.Services.Impl
{
    public class GCNotificationService : INotificationService
    {
        private readonly string reference = "Simple form service email notification";
        private readonly OpenshiftConfig _openshiftConfig;
        
        public GCNotificationService(OpenshiftConfig openshiftConfig)
        {
            _openshiftConfig = openshiftConfig;
        }

        /// <summary>
        /// Send email notification to Admin with a URL to the form.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="templateId"></param>
        /// <param name="formId"></param>
        /// <param name="submissionId"></param>
        /// <param name="emailAddresses"></param>
        /// <returns></returns>
        public EmailNotificationResponse SendNotification(NotificationClient client, string templateId, string formId, string submissionId, List<string> emailAddresses)
        {
            client = new NotificationClient(_openshiftConfig.GCNotify_BaseURL, _openshiftConfig.GCNotify_ApiKey);
            EmailNotificationResponse response = new EmailNotificationResponse();
            int successCount = 0;
            int failedCount = 0;

            Dictionary<String, dynamic> personalisation = new Dictionary<String, dynamic>
            {
                {"url", "https://www.test.com"}
            };
            if (emailAddresses != null && emailAddresses.Count > 0)
            {
                Console.WriteLine("===== INFO: Ready to send email to Admin... Form ID: " + formId + " Submission Id: " + submissionId);
                foreach (string email in emailAddresses)
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
