using System.ComponentModel;
using System.Text.Json.Serialization;
using SimpleFormsService.Domain.Entities.Base;
using SimpleFormsService.Domain.Entities.Supporting.JSON;

namespace SimpleFormsService.Domain.Entities
{
    public class FormSubmission : EntityBase
    {
        public FormSubmission() : base(Guid.NewGuid())
        {}

        public FormSubmission(Guid id, Guid templateId, FormSubmissionData? formSubmissionData) : base(id)
        {
            TemplateId = templateId;
            Data = formSubmissionData;
        }

        [JsonPropertyName("submissionId")] public new Guid Id => base.Id;
        [JsonIgnore] [ReadOnly(true)]  public virtual FormTemplate Template { get; private set; }
        [JsonPropertyOrder(1)] public Guid TemplateId { get; set; }
        [JsonPropertyOrder(2)] public FormSubmissionData? Data { get; set; }
    }
}