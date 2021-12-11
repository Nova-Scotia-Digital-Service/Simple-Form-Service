using System.Text.Json;

namespace SimpleFormsService.Contract.Dtos.FormSubmission
{
    public class FormSubmissionDto
    {
        public string Id { get; set; }
        public string TemplateId { get; set; }
        public string SubmissionData { get; set; }
    }
}