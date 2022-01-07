using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleFormsService.Domain.Entities.Supporting.JSON;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace SimpleFormsService.Web.Public.Forms.SpecialPatientProgram
{
    public class SPPForm
    {
        public string SubmissionId { get; set; }

        [Display(Name = "Label_Email", ResourceType = typeof(StringResource))]
        [Required(ErrorMessageResourceName = "Email_RequiredErr", ErrorMessageResourceType = typeof(StringResource))]
        [EmailAddress(ErrorMessageResourceName = "Email_FormatErr", ErrorMessageResourceType = typeof(StringResource))]
        [StringLength(150, ErrorMessageResourceName = "Email_LengthErr", ErrorMessageResourceType = typeof(StringResource))]
        public string Email { get; set; }

        [Display(Name = "Label_ConfirmEmail", ResourceType = typeof(StringResource))]
        [Required(ErrorMessageResourceName = "ConfirmEmail_RequiredErr", ErrorMessageResourceType = typeof(StringResource))]
        [EmailAddress(ErrorMessageResourceName = "ConfirmEmail_FormatErr", ErrorMessageResourceType = typeof(StringResource))]
        [Compare("Email", ErrorMessageResourceName = "ConfirmEmail_CompareErr", ErrorMessageResourceType = typeof(StringResource))]
        [StringLength(150, ErrorMessageResourceName = "ConfirmEmail_LengthErr", ErrorMessageResourceType = typeof(StringResource))]
        public string ConfirmEmail { get; set; }

        [Display(Name = "Label_Name", ResourceType = typeof(StringResource))]
        [StringLength(150, MinimumLength = 5, ErrorMessageResourceName = "Name_LengthErr", ErrorMessageResourceType = typeof(StringResource))]
        public string Name { get; set; }
        
        [Display(Name = "Label_Phone", ResourceType = typeof(StringResource))]
        [Phone(ErrorMessageResourceName = "Phone_FormatErr", ErrorMessageResourceType = typeof(StringResource))]
        [StringLength(14, ErrorMessageResourceName = "Phone_LengthErr", ErrorMessageResourceType = typeof(StringResource))]
        public string Phone { get; set; }

        [Display(Name = "Label_SubmissionType", ResourceType = typeof(StringResource))]
        [Required(ErrorMessageResourceName = "SubmissionType_RequiredErr", ErrorMessageResourceType = typeof(StringResource))]        
        public string SubmissionType { get; set; }

        public virtual List<SelectListItem> SubmissionTypes { get; set; }

        [DataType(DataType.Upload)]
        //[Required(ErrorMessageResourceName = "Upload_RequiredErr", ErrorMessageResourceType = typeof(StringResource))]
        //KDA - temporarily removing the required validation for File Upload
        public List<IFormFile> Files { get; set; }

        [Required(ErrorMessageResourceName = "Upload_RequiredErr", ErrorMessageResourceType = typeof(StringResource))]
        public Dictionary<string,string> UploadedFiles { get; set; }

        public FormItem[] GetFormItems()
        {
            return new FormItem[]
            {
                new FormItem("Email", Email),
                new FormItem("Name",Name),
                new FormItem("Phone",Phone),
                new FormItem("SubmissionType",SubmissionType)
            };
        }

    }
}
