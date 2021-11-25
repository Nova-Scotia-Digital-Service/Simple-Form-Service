using SimpleFormsService.Web.Admin.Resources;
using System.ComponentModel.DataAnnotations;

namespace SimpleFormsService.Web.Admin.Pages
{
    public class FormInfo
    {
        [Display(Name = "Label_SubmissionID", ResourceType = typeof(StringResource))]
        public string SubmissionID { get; set; }

        [Display(Name = "Label_FormID", ResourceType = typeof(StringResource))]
        public string FormID { get; set; }

        [Display(Name = "Label_SubmissionDate", ResourceType = typeof(StringResource))]
        public DateTime SubmissionDate { get; set; }
    }
}
