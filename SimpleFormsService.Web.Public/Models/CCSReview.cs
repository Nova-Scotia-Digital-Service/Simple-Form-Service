using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SimpleFormsService.Web.Public.Views.Shared;
using SimpleFormsService.Web.Public.Views.Forms.ChildCareSubsidyReview;
using SimpleFormsService.Domain.Entities.Supporting.JSON;
using System.Linq;

namespace SimpleFormsService.Web.Public.Models
{
    [ValidateAntiForgeryToken(Order = 1000)]
    public class CCSReview : BaseForm, IForm
    {        
        [Display(Name = "Label_ApplicantEmail", ResourceType = typeof(StringResource))]
        [Required(ErrorMessageResourceName = "FormItem_FieldRequiredErr", ErrorMessageResourceType = typeof(SharedResource))]
        [EmailAddress(ErrorMessageResourceName = "FormItem_EmailInvalidErr", ErrorMessageResourceType = typeof(SharedResource))]
        [StringLength(150, ErrorMessageResourceName = "FormItem_EmailStringLengthErr", ErrorMessageResourceType = typeof(SharedResource))]
        public string ApplicantEmail { get; set; }

        [Display(Name = "Label_ApplicantName", ResourceType = typeof(StringResource))]
        [StringLength(150, ErrorMessageResourceName = "FormItem_NameStringLengthErr", ErrorMessageResourceType = typeof(SharedResource))]
        public string ApplicantName { get; set; }

        [Display(Name = "Label_CaseWorker", ResourceType = typeof(StringResource))]
        [Required(ErrorMessageResourceName = "FormItem_SelectRequiredErr", ErrorMessageResourceType = typeof(SharedResource))]
        public string CaseWorker { get; set; }

        //public virtual IList<SelectListItem> CaseWorkers { get; set; }

        public IList<SelectListItem> CaseWorkers()
        {
            IList<SelectListItem> names = new List<SelectListItem>()
            {
                new SelectListItem{Value="Jane Doe", Text="Jane Doe" },
                new SelectListItem{Value="John Doe", Text="John Doe" },
                new SelectListItem{Value="John Smith", Text="John Smith" }
            };

            return names;
        }

        [Display(Name = "Label_SupportingDocument", ResourceType = typeof(StringResource))]
        [Required(ErrorMessageResourceName = "FormItem_SelectRequiredErr", ErrorMessageResourceType = typeof(SharedResource))]
        public string SupportingDocument { get; set; }

        //public virtual List<ListItem> SupportingDocuments { get; set; }
        public List<ListItem> SupportDocuments()
        {
            List<ListItem> documents = new()
            {
                new ListItem { Description = StringResource.CCSFormat_Paper, Code = "P", Hint = StringResource.CCSDelivery_Paper },
                new ListItem { Description = StringResource.CCSFormat_Digital, Code = "D", Hint = StringResource.CCSDelivery_Digital },
                new ListItem { Description = StringResource.CCSFormat_Both, Code = "Both", Hint = StringResource.CCSDelivery_Both },
            };

            return documents;
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
                new FormItem("Case Worker", CaseWorker),
                new FormItem("Applicant Email",ApplicantEmail),
                new FormItem("Applicant Name",ApplicantName),
                new FormItem("Supporting Document",SupportingDocument)
            };
        }

        public void SetFormItems(FormItem[] formItems)
        {
            var formItemList = formItems.ToList<FormItem>();

            CaseWorker = formItemList.SingleOrDefault(x => x.Name == "Case Worker")?.Value;
            ApplicantEmail = formItemList.SingleOrDefault(x => x.Name == "Applicant Email")?.Value;
            ApplicantName = formItemList.SingleOrDefault(x => x.Name == "Applicant Name")?.Value;
            SupportingDocument = formItemList.SingleOrDefault(x => x.Name == "Supporting Document")?.Value;
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
                new NotifyEmailAddress(ApplicantEmail)
            };
        }
    }
}
