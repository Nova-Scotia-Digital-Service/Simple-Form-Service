using SimpleFormsService.Web.Public.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleFormsService.Web.Public.Models
{
    [ValidateAntiForgeryToken(Order = 1000)]
    public class CCSForm
    {
        [Display(Name = "Label_RegionalOffice", ResourceType = typeof(StringResource_CCSForm))] 
        public string RegionalOffice { get; set; }

        [Required(ErrorMessageResourceName = "ApplicantEmail_RequiredErr", ErrorMessageResourceType = typeof(StringResource_CCSForm))]
        [Display(Name = "Label_ApplicantEmail", ResourceType = typeof(StringResource_CCSForm))]
        public string ApplicantEmail { get; set; }

        [Display(Name = "Label_ApplicantName", ResourceType = typeof(StringResource_CCSForm))] 
        public string ApplicantName { get; set; }
        
        [Display(Name = "Label_SupportingDocument", ResourceType = typeof(StringResource_CCSForm))]
        public string SupportingDocument { get; set; }
        public virtual List<SelectListItem> RegionalOffices { get; set; }
        public virtual List<ListItem> SupportingDocuments { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile File { get; set; }
    }
}
