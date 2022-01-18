using Notify.Client;
using Notify.Models.Responses;
using SimpleFormsService.Services.Abstractions.Application;

namespace SimpleFormsService.Services.Application
{
    public class GCNotificationService : ServiceBase ,INotificationService
    {
        private readonly NotificationClient _client;
        private const string _reference = "Simple form service email notification";

        public GCNotificationService(NotificationClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Send email notification to Admin with a URL to the form.
        /// </summary>
        /// <param name="gcNotifyTemplateId"></param>
        /// <param name="formTemplateId"></param>
        /// <param name="formSubmissionId"></param>
        /// <param name="emailAddresses"></param>
        /// <returns></returns>
        public EmailNotificationResponse SendNotification(string gcNotifyTemplateId, string formTemplateId, string formSubmissionId, List<string> emailAddresses) 
        {
            var response = new EmailNotificationResponse();
            var successCount = 0;
            var failedCount = 0;

            var personalisation = new Dictionary<string, dynamic>
            {
                {"url", "https://www.test.com"}
            };

            if (emailAddresses != null && emailAddresses.Count > 0)
            {
                Console.WriteLine("===== INFO: Ready to send email to Admin... Form ID: " + formTemplateId + " Submission Id: " + formSubmissionId);
                foreach (var email in emailAddresses)
                {
                    try
                    {
                        response = _client.SendEmail(email, gcNotifyTemplateId, personalisation, _reference);
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
