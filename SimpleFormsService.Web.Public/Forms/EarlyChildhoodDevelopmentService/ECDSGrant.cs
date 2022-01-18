using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleFormsService.Web.Public.Forms.EarlyChildhoodDevelopmentService
{
    [ValidateAntiForgeryToken(Order = 1000)]
    public class ECDSGrant
    {
        [Required(ErrorMessageResourceName = "Recipients_RequiredErr", ErrorMessageResourceType = typeof(StringResource))]
        public string Recipient { get; set; }

        [Required(ErrorMessageResourceName = "CentreEmail_RequiredErr", ErrorMessageResourceType = typeof(StringResource))]
        [Display(Name = "Label_CentreEmail", ResourceType = typeof(StringResource))]
        public string CentreEmail { get; set; }

        [Display(Name = "Label_CentreName", ResourceType = typeof(StringResource))] 
        public string CentreName { get; set; }

        public string CentreId { get; set; }
        public virtual IList<SelectListItem> Consultants { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile File { get; set; }
    }
}
