using Notify.Client;
using Notify.Models.Responses;
using SimpleFormsService.API.Global;

namespace SimpleFormsService.API.Services.Impl
{
    public class ExchangeNotificationService : INotificationService
    {
        private readonly string reference = "Simple form service email notification";
        private readonly OpenshiftConfig _openshiftConfig;
        
        public ExchangeNotificationService(OpenshiftConfig openshiftConfig)
        {
            _openshiftConfig = openshiftConfig;
        }

        /// <summary>
        /// Send email notification to Admin with a URL to the form.
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="submissionID"></param>
        /// <returns></returns>
        public EmailNotificationResponse sendNotification(NotificationClient client, string templateId, string formId, string submissionId)
        {
            client = new NotificationClient(_openshiftConfig.GCNotifyBaseURL, _openshiftConfig.GCNotifyApiKey);
            EmailNotificationResponse response = new EmailNotificationResponse();
                     
            Dictionary<String, dynamic> personalisation = new Dictionary<String, dynamic>
            {
                {"url", "https://www.test.com"}
            };
            try
            {
                Console.WriteLine("Ready to send email to Admin... Form ID: " + formId  + " Submission Id: " + submissionId);
                response = client.SendEmail("admin email address", templateId, personalisation, reference);       
            }
            catch
            {
                Console.WriteLine("ERROR: Unable to send email to Admin: " + "email");
            }
            
            return response;
        }
    }
}
