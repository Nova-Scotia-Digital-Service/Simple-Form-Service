﻿using SimpleFormsService.Web.Public.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleFormsService.Web.Public.Models
{
    [ValidateAntiForgeryToken(Order = 1000)]
    public class ECDSGrant
    {
        public string Consultant { get; set; }

        [Required(ErrorMessageResourceName = "CentreEmail_RequiredErr", ErrorMessageResourceType = typeof(StringResource_ECDSGrants))]
        [Display(Name = "Label_CentreEmail", ResourceType = typeof(StringResource_ECDSGrants))]
        public string CentreEmail { get; set; }

        [Display(Name = "Label_CentreName", ResourceType = typeof(StringResource_ECDSGrants))] 
        public string CentreName { get; set; }

        public string CentreId { get; set; }
        public virtual IList<SelectListItem> Consultants { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile File { get; set; }
    }
}
