using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SimpleFormsService.Web.Public.Views.Shared;
using SimpleFormsService.Web.Public.Views.Forms.DrugInformationSystem;
using System.Linq;
using SimpleFormsService.Domain.Entities.Supporting.JSON;

namespace SimpleFormsService.Web.Public.Models
{
    [ValidateAntiForgeryToken(Order = 1000)]
    public class DISPharmacy : BaseForm, IForm
    {
        [Display(Name = "Label_DISEmail", ResourceType = typeof(StringResource))]
        [Required(ErrorMessageResourceName = "FormItem_FieldRequiredErr", ErrorMessageResourceType = typeof(SharedResource))]
        [EmailAddress(ErrorMessageResourceName = "FormItem_EmailInvalidErr", ErrorMessageResourceType = typeof(SharedResource))]
        [StringLength(150, ErrorMessageResourceName = "FormItem_EmailStringLengthErr", ErrorMessageResourceType = typeof(SharedResource))]
        public string Email { get; set; }

        [Display(Name = "Label_Subject", ResourceType = typeof(StringResource))]
        [Required(ErrorMessageResourceName = "FormItem_FieldRequiredErr", ErrorMessageResourceType = typeof(SharedResource))]
        [StringLength(150, ErrorMessageResourceName = "FormItem_NameStringLengthErr", ErrorMessageResourceType = typeof(SharedResource))]

        public string Subject { get; set; }

        [Display(Name = "Label_Contact", ResourceType = typeof(StringResource))]
        [Required(ErrorMessageResourceName = "FormItem_FieldRequiredErr", ErrorMessageResourceType = typeof(SharedResource))]
        [StringLength(150, ErrorMessageResourceName = "FormItem_TextStringLengthErr", ErrorMessageResourceType = typeof(SharedResource))]
        public string Contact { get; set; }

        //[Display(Name = "Label_SourceType", ResourceType = typeof(StringResource))]
        //[Required(ErrorMessageResourceName = "FormItem_SelectRequiredErr", ErrorMessageResourceType = typeof(SharedResource))]
        //public string SourceType { get; set; }

        [Display(Name = "Label_Source", ResourceType = typeof(StringResource))]
        [Required(ErrorMessageResourceName = "FormItem_FieldRequiredErr", ErrorMessageResourceType = typeof(SharedResource))]
        [StringLength(150, ErrorMessageResourceName = "FormItem_NameStringLengthErr", ErrorMessageResourceType = typeof(SharedResource))]
        public string SourceName { get; set; }

        [Display(Name = "Label_DISTicket", ResourceType = typeof(StringResource))]
        [StringLength(150, ErrorMessageResourceName = "FormItem_NumberStringLengthErr", ErrorMessageResourceType = typeof(SharedResource))]
        public string TicketNumber { get; set; }

        [Display(Name = "Label_VendorTicketNumber", ResourceType = typeof(StringResource))]
        [StringLength(150, ErrorMessageResourceName = "FormItem_NumberStringLengthErr", ErrorMessageResourceType = typeof(SharedResource))]
        public string VendorTicketNumber { get; set; }

        [Display(Name = "Label_Message", ResourceType = typeof(StringResource))]
        [DataType(DataType.MultilineText)]
        [StringLength(2000, ErrorMessageResourceName = "FormItem_TextareaStringLengthErr", ErrorMessageResourceType = typeof(SharedResource))]
        public string Message { get; set; }

        //public virtual List<SelectListItem> SourceTypes { get; set; }
        //public List<SelectListItem> SourceTypes()
        //{
        //    List<SelectListItem> names = new()
        //    {
        //        new SelectListItem { Value = StringResource.Label_Pharmacy, Text = "Pharmacy" },
        //        new SelectListItem { Value = StringResource.Label_Vendor, Text = "Vendor" },
        //        new SelectListItem { Value = StringResource.Label_Org, Text = "Organization" }
        //    };

        //    return names.OrderBy(x => x.Value).ToList();
        //}

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
                new FormItem("Subject",Subject),               
                //new FormItem("SourceType",SourceType),                
                new FormItem("Source Name",SourceName),
                new FormItem("Contact",Contact),
                new FormItem("Ticket Number",TicketNumber),
                new FormItem("Vendor Ticket Number",VendorTicketNumber),
                new FormItem("Message",Message)
            };
        }

        public void SetFormItems(FormItem[] formItems)
        {
            var formItemList = formItems.ToList<FormItem>();

            Email = formItemList.SingleOrDefault(x => x.Name == "Email")?.Value;
            Subject = formItemList.SingleOrDefault(x => x.Name == "Subject")?.Value;
            //SourceType = formItemList.SingleOrDefault(x => x.Name == "SourceType")?.Value;
            SourceName = formItemList.SingleOrDefault(x => x.Name == "Source Name")?.Value;
            Contact = formItemList.SingleOrDefault(x => x.Name == "Contact")?.Value;
            TicketNumber = formItemList.SingleOrDefault(x => x.Name == "Ticket Number")?.Value;
            VendorTicketNumber = formItemList.SingleOrDefault(x => x.Name == "Vendor Ticket Number")?.Value;
            Message = formItemList.SingleOrDefault(x => x.Name == "Message")?.Value;
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
