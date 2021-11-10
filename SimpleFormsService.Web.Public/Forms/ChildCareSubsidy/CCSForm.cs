using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleFormsService.Web.Public.Forms.Shared;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleFormsService.Web.Public.Forms.ChildCareSubsidy
{
    [ValidateAntiForgeryToken(Order = 1000)]
    public class CCSForm
    {
        [Display(Name = "Label_RegionalOffice", ResourceType = typeof(StringResource))] 
        public string RegionalOffice { get; set; }

        [Required(ErrorMessageResourceName = "ApplicantEmail_RequiredErr", ErrorMessageResourceType = typeof(StringResource))]
        [Display(Name = "Label_ApplicantEmail", ResourceType = typeof(StringResource))]
        public string ApplicantEmail { get; set; }

        [Display(Name = "Label_ApplicantName", ResourceType = typeof(StringResource))] 
        public string ApplicantName { get; set; }
        
        [Display(Name = "Label_SupportingDocument", ResourceType = typeof(StringResource))]
        public string SupportingDocument { get; set; }
        public virtual List<SelectListItem> RegionalOffices { get; set; }
        public virtual List<ListItem> SupportingDocuments { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile File { get; set; }
    }
}
