using SimpleFormsService.Domain.Entities;
using SimpleFormsService.Domain.Entities.Supporting.JSON;
using SimpleFormsService.Web.Admin.Resources;
using System.ComponentModel.DataAnnotations;

namespace SimpleFormsService.Web.Admin.Models.Admin
{
    public class SubmissionDetailModel
    {
        [Display(Name = "Label_SubmissionID", ResourceType = typeof(StringResource))]
        public string SubmissionID { get; set; }

        [Display(Name = "Label_TemplateID", ResourceType = typeof(StringResource))]
        public string TemplateID { get; set; }

        [Display(Name = "Label_SubmissionDate", ResourceType = typeof(StringResource))]
        public DateTime SubmissionDate { get; set; }
        public FormSubmission FormSubmission { get; set; }
        public FormSubmissionData SubmissionData { get; set; }
        public FormItem[] FormItems { get; set; }
        public DocumentReference[] Documents { get; set; }
    }
}
