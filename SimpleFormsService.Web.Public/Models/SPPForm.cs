using SimpleFormsService.Web.Public.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace SimpleFormsService.Web.Public.Models
{
    public class SPPForm
    {
        [Required(ErrorMessageResourceName = "SubmitterEmail_RequiredErr", ErrorMessageResourceType = typeof(StringResource_SPP))]
        [Display(Name = "Label_Email", ResourceType = typeof(StringResource_SPP))]
        public string Email { get; set; }

        [Display(Name = "Label_FirstName", ResourceType = typeof(StringResource_SPP))]
        public string FirstName { get; set; }

        [Display(Name = "Label_LastName", ResourceType = typeof(StringResource_SPP))]
        public string LastName { get; set; }

        [Display(Name = "Label_Phone", ResourceType = typeof(StringResource_SPP))]
        public string Phone { get; set; }

        [Display(Name = "Label_SubmissionType", ResourceType = typeof(StringResource_SPP))] 
        public string SubmissionType { get; set; }

        public virtual List<SelectListItem> SubmissionTypes { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile File { get; set; }

    }
}
