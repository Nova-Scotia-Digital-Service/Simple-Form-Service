using SimpleFormsService.Domain.Entities.Base;
using SimpleFormsService.Domain.Entities.FormSubmission.Supporting.JSON;

namespace SimpleFormsService.Domain.Entities.FormSubmission
{
    public class FormSubmission : EntityBase
    {
        public FormSubmission(Guid id, Guid templateId, FormSubmissionData? submissionData) : base(id)
        {
            TemplateId = templateId;
            SubmissionData = submissionData;
        }

        public Guid TemplateId { get; set; }
        public FormSubmissionData? SubmissionData { get; set; } // POCO Mapping https://www.npgsql.org/efcore/mapping/json.html?tabs=fluent-api%2Cpoco#tabpanel_4MQ50Ciht1_fluent-api

        /*
         {
            "DateSubmitted": "2021-12-09 17:07:25.00000+00",
            "SubmissionStatus": "INITIALIZED",
            "NotifyEmailAddresses": [
                { "EmailAddress": "notify1@novascotia.ca" },
                { "EmailAddress": "notify2@novascotia.ca" },
                { "EmailAddress": "notify3@novascotia.ca" }
            ],
            "FormItems": [
                { "Name": "Email", "Value": "johndoe@gmail.com" },
                { "Name": "Name", "Value": "John Doe" },
                { "Name": "Phone", "Value": "902-000-000" },
                { "Name": "Submission Type", "Value": "New Application" }
            ],
            "DocumentReferences": [
                { "URI": "/pathtodocumentminusthehost/1" },
                { "URI": "/pathtodocumentminusthehost/2" }
            ]
        }
         */
    }
}
