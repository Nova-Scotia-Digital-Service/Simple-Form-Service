namespace SimpleFormsService.API.Model
{
    using System.Text.Json.Serialization;
    public class FormModel
    {
        public FormModel(string formId, string submissionId)
        {
            FormId = formId;
            SubmissionId = submissionId;
        }

        [JsonPropertyName("formId")]
        public string FormId { get; set; }

        [JsonPropertyName("submissionId")]
        public string SubmissionId { get; set; }


        //form data will be the contents of the submitted form without the file uploads
        [JsonPropertyName("formData")]
        public string FormData { get; set;  }

    }



}
