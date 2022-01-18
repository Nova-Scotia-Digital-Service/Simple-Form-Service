using SimpleFormsService.Web.Admin.Resources;
using System.ComponentModel.DataAnnotations;

namespace SimpleFormsService.Web.Admin.Models
{
    public class SearchSubmission
    {
        public string? Criteria { get; set; }

        [Display(Name = "Label_Status", ResourceType = typeof(StringResource))]
        public string? Status { get; set; }
    }
}
