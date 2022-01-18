using Notify.Client;
using Notify.Models.Responses;
using SimpleFormsService.Configuration;
using SimpleFormsService.Domain.Entities;
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
        /// Send email confirmation notification to the public user.
        /// </summary>
        /// <param name="formTemplate"></param>
        /// <param name="formSubmission"></param>
        /// <param name="emailAddresses"></param>
        /// <returns></returns>
        public EmailNotificationResponse SendConfirmationNotification(FormTemplate formTemplate, FormSubmission formSubmission, List<string> emailAddresses) 
        {
            var response = new EmailNotificationResponse();
            var successCount = 0;
            var failedCount = 0;
            
            var personalisation = new Dictionary<string, dynamic>
            {
                {"template-id-friendly", formTemplate.Data.Identifier.FriendlyName },
                {"submission-id-friendly", formSubmission.Data?.Identifier.FriendlyName},
            };

            response = EmailNotificationResponse(OpenshiftConfig.GCNotify_User_TemplateId, formTemplate, formSubmission, emailAddresses, response, personalisation, ref successCount, ref failedCount);
            
            Console.WriteLine("====== INFO: Total failed count equals to" + failedCount + "; Success count equals to " + successCount + " ========");
            return response;
        }

        /// <summary>
        /// Send email admin notification to the public user.
        /// </summary>
        /// <param name="formTemplate"></param>
        /// <param name="formSubmission"></param>
        /// <param name="emailAddresses"></param>
        /// <returns></returns>
        public EmailNotificationResponse SendAdminNotification(FormTemplate formTemplate, FormSubmission formSubmission, List<string> emailAddresses)
        {
            var response = new EmailNotificationResponse();
            var successCount = 0;
            var failedCount = 0;

            var personalisation = new Dictionary<string, dynamic>
            {
                {"url", $"{OpenshiftConfig.GCNotify_Admin_Base_URL}/{formTemplate.Id}/{formSubmission.Id}" },
                {"template-id-friendly", formTemplate.Data.Identifier.FriendlyName },
                {"submission-id-friendly", formSubmission.Data?.Identifier.FriendlyName},
                {"routing-info","" }
            };

            response = EmailNotificationResponse(OpenshiftConfig.GCNotify_Admin_TemplateId, formTemplate, formSubmission, emailAddresses, response, personalisation, ref successCount, ref failedCount);

            Console.WriteLine("====== INFO: Total failed count equals to" + failedCount + "; Success count equals to " + successCount + " ========");
            return response;
        }

        #region Private Helpers
        private EmailNotificationResponse EmailNotificationResponse(string gcNotifyTemplateId, FormTemplate formTemplate, FormSubmission formSubmission, List<string> emailAddresses, EmailNotificationResponse response, Dictionary<string, dynamic> personalisation, ref int successCount, ref int failedCount)
        {
            if (emailAddresses != null && emailAddresses.Count > 0)
            {
                Console.WriteLine("===== INFO: Ready to send email. Template Id: " + formTemplate.Id + " Submission Id: " + formSubmission.Id);
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

            return response;
        }

        #endregion
    }
}
