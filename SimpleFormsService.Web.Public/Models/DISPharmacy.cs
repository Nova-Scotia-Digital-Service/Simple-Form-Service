using SimpleFormsService.Web.Public.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleFormsService.Web.Public.Models
{
    [ValidateAntiForgeryToken(Order = 1000)]
    public class DISPharmacy
    {
        [Display(Name = "Label_Contact", ResourceType = typeof(StringResource_DISPharmacy))]
        [Required(ErrorMessageResourceName = "Contact_RequiredErr", ErrorMessageResourceType = typeof(StringResource_DISPharmacy))]
        public string Contact { get; set; }

        [Required(ErrorMessageResourceName = "Subject_RequiredErr", ErrorMessageResourceType = typeof(StringResource_DISPharmacy))]
        [Display(Name = "Label_Subject", ResourceType = typeof(StringResource_DISPharmacy))]
        public string Subject { get; set; }

        [Required(ErrorMessageResourceName = "DISEmail_RequiredErr", ErrorMessageResourceType = typeof(StringResource_DISPharmacy))]
        [Display(Name = "Label_DISEmail", ResourceType = typeof(StringResource_DISPharmacy))] 
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "Source_RequiredErr", ErrorMessageResourceType = typeof(StringResource_DISPharmacy))]
        [Display(Name = "Label_Source", ResourceType = typeof(StringResource_DISPharmacy))]
        public string SourceName { get; set; }

        [Display(Name = "Label_SourceType", ResourceType = typeof(StringResource_DISPharmacy))]
        public string SourceType { get; set; }

        [Display(Name = "Label_DISTicket", ResourceType = typeof(StringResource_DISPharmacy))] 
        public string TicketNumber { get; set; }

        [Display(Name = "Label_VendorTicketNumber", ResourceType = typeof(StringResource_DISPharmacy))]
        public string VendorTicketNumber { get; set; }

        public virtual List<SelectListItem> SourceTypes { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile File { get; set; }
    }
}
