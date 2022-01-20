using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleFormsService.Domain.Entities.Supporting.JSON;
//using SimpleFormsService.Web.Public.Forms.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SimpleFormsService.Web.Public.Views.Shared;
using SimpleFormsService.Web.Public.Views.Forms.SpecialPatientProgram;

namespace SimpleFormsService.Web.Public.Models
{
    public class SPPForm : BaseForm, IForm
    {
        [Display(Name = "Label_Email", ResourceType = typeof(StringResource))]
        [Required(ErrorMessageResourceName = "FormItem_FieldRequiredErr", ErrorMessageResourceType = typeof(SharedResource))]
        [EmailAddress(ErrorMessageResourceName = "FormItem_EmailInvalidErr", ErrorMessageResourceType = typeof(SharedResource))]
        [StringLength(150, ErrorMessageResourceName = "FormItem_EmailStringLengthErr", ErrorMessageResourceType = typeof(SharedResource))]
        public string Email { get; set; }

        [Display(Name = "Label_Name", ResourceType = typeof(StringResource))]
        [StringLength(150, ErrorMessageResourceName = "FormItem_NameStringLengthErr", ErrorMessageResourceType = typeof(SharedResource))]
        public string Name { get; set; }

        [Display(Name = "Label_Phone", ResourceType = typeof(StringResource))]
        [Phone(ErrorMessageResourceName = "FormItem_PhoneInvalidErr", ErrorMessageResourceType = typeof(SharedResource))]
        [StringLength(14, ErrorMessageResourceName = "FormItem_PhoneStringLengthErr", ErrorMessageResourceType = typeof(SharedResource))]
        public string Phone { get; set; }

        [Display(Name = "Label_SubmissionType", ResourceType = typeof(StringResource))]
        [Required(ErrorMessageResourceName = "FormItem_SelectRequiredErr", ErrorMessageResourceType = typeof(SharedResource))]
        public string SubmissionType { get; set; }

        //public virtual List<SelectListItem> SubmissionTypes { get; set; }

        public List<SelectListItem> SubmissionTypes()
        {
            List<SelectListItem> items = new()
            {
                new SelectListItem { Text = StringResource.SubmissionType_New, Value = "new" },
                new SelectListItem { Text = StringResource.SubmissionType_Existing, Value = "existing" }
            };

            return items.OrderBy(x => x.Text).ToList();
        }

        [DataType(DataType.Upload)]
        public List<IFormFile> Files { get; set; }

        [Range(1, int.MaxValue, ErrorMessageResourceName = "Upload_RequiredErr", ErrorMessageResourceType = typeof(SharedResource))]
        public int NumberOfUploadedFiles { get { return UploadedFiles?.Count ?? 0; } }

        public Dictionary<string, string> UploadedFiles { get; set; }

        public FormItem[] GetFormItems()
        {
            return new FormItem[]
            {
                new FormItem("Email", Email),
                new FormItem("Name",Name),
                new FormItem("Phone",Phone),
                new FormItem("Submission Type",SubmissionType)
            };
        }

        public void SetFormItems(FormItem[] formItems)
        {
            var formItemList = formItems.ToList<FormItem>();

            Email = formItemList.SingleOrDefault(x => x.Name == "Email")?.Value;
            Name = formItemList.SingleOrDefault(x => x.Name == "Name")?.Value;
            Phone = formItemList.SingleOrDefault(x => x.Name == "Phone")?.Value;
            SubmissionType = formItemList.SingleOrDefault(x => x.Name == "Submission Type")?.Value;
        }

        public DocumentReference[] GetFormFiles()
        {
            return UploadedFiles.Select(x => new DocumentReference(TemplateId, x.Key, x.Value)).ToArray<DocumentReference>();
        }

        public void SetFormFiles(DocumentReference[] documentReferences)
        {
            if (documentReferences != null)
            {
                UploadedFiles = new Dictionary<string, string>();
                foreach (var docRef in documentReferences)
                {
                    UploadedFiles.Add(docRef.DocumentId, docRef.Filename);
                };
            }
        }

        public NotifyEmailAddress[] GetConfirmationEmailAddresses()
        {
            return new NotifyEmailAddress[]
            {
                new NotifyEmailAddress(Email)
            };
        }
    }
}
