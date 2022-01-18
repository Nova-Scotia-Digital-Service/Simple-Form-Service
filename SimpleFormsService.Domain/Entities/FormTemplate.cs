using System.Text.Json.Serialization;
using SimpleFormsService.Domain.Entities.Base;
using SimpleFormsService.Domain.Entities.Supporting.JSON;

namespace SimpleFormsService.Domain.Entities
{
    public class FormTemplate : EntityBase
    {
        public FormTemplate() : base(Guid.NewGuid())
        {}

        public FormTemplate(Guid id, FormTemplateData formTemplateData) : base(id)
        {
            Data = formTemplateData; 
        }

        [JsonPropertyOrder(1)] public FormTemplateData Data { get; set; }
    }
}