using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleFormsService.Web.Public.Forms.ChildCareSubsidyReview
{
    [ValidateAntiForgeryToken(Order = 1000)]
    public class CCSReview
    {
        [Display(Name = "Label_CaseWorker", ResourceType = typeof(StringResource))] 
        public string CaseWorker { get; set; }

        [Required(ErrorMessageResourceName = "ApplicantEmail_RequiredErr", ErrorMessageResourceType = typeof(StringResource))]
        [Display(Name = "Label_ApplicantEmail", ResourceType = typeof(StringResource))]
        public string ApplicantEmail { get; set; }

        [Display(Name = "Label_ApplicantName", ResourceType = typeof(StringResource))] 
        public string ApplicantName { get; set; }

        public virtual IList<SelectListItem> CaseWorkers { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile File { get; set; }
    }
}
