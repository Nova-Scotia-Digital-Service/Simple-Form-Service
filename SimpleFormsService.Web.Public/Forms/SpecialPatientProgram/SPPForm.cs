using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleFormsService.Domain.Entities.Supporting.JSON;
using SimpleFormsService.Web.Public.Forms.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SimpleFormsService.Web.Public.Forms.SpecialPatientProgram
{
    public class SPPForm : IForm
    {
        public string SubmissionId { get; set; }
        public string TemplateId { get; set; }
        public string FileIdToDelete { get; set; }

        [Display(Name = "Label_Email", ResourceType = typeof(StringResource))]
        [Required(ErrorMessageResourceName = "Email_RequiredErr", ErrorMessageResourceType = typeof(StringResource))]
        [EmailAddress(ErrorMessageResourceName = "Email_FormatErr", ErrorMessageResourceType = typeof(StringResource))]
        [StringLength(150, ErrorMessageResourceName = "Email_LengthErr", ErrorMessageResourceType = typeof(StringResource))]
        public string Email { get; set; }

        [Display(Name = "Label_Name", ResourceType = typeof(StringResource))]
        [StringLength(150, ErrorMessageResourceName = "Name_LengthErr", ErrorMessageResourceType = typeof(StringResource))]
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
        public List<IFormFile> Files { get; set; }

        [Range(1, int.MaxValue, ErrorMessageResourceName = "Upload_RequiredErr", ErrorMessageResourceType = typeof(StringResource))]
        public int NumberOfUploadedFiles { get { return UploadedFiles?.Count ?? 0; } }

        public Dictionary<string, string> UploadedFiles { get; set; }

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

        public NotifyEmailAddress[] GetConfirmationEmailAddresses()
        {
            return new NotifyEmailAddress[]
            {
                new NotifyEmailAddress(Email)
            };
        }

        public void SetFormItems(FormItem[] formItems)
        {
            var formItemList = formItems.ToList<FormItem>();

            Email = formItemList.SingleOrDefault(x => x.Name == "Email")?.Value;
            Name = formItemList.SingleOrDefault(x => x.Name == "Name")?.Value;
            Phone = formItemList.SingleOrDefault(x => x.Name == "Phone")?.Value;
            SubmissionType = formItemList.SingleOrDefault(x => x.Name == "SubmissionType")?.Value;
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
    }
}
