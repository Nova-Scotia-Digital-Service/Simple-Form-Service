namespace SimpleFormsService.API.Model
{
    using System.Text.Json.Serialization;
    public class FormModel
    {
        public FormModel(string templateId, string submissionId)
        {
            TempateId = templateId;
            SubmissionId = submissionId;
        }

        [JsonPropertyName("Template_Id")]
        public string TempateId { get; set; }

        [JsonPropertyName("submissionId")]
        public string SubmissionId { get; set; }


        //form data will be the contents of the submitted form without the file uploads
        [JsonPropertyName("formData")]
        public string FormData { get; set;  }

    }



}
