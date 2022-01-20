using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SimpleFormsService.Web.Public.Views.Shared;
using SimpleFormsService.Web.Public.Views.Forms.EarlyChildhoodDevelopmentService;
using System.Linq;
using SimpleFormsService.Domain.Entities.Supporting.JSON;

namespace SimpleFormsService.Web.Public.Models
{
    [ValidateAntiForgeryToken(Order = 1000)]
    public class ECDSGrant : BaseForm, IForm
    {
        [Display(Name = "Label_Consultant", ResourceType = typeof(StringResource))]
        [Required(ErrorMessageResourceName = "FormItem_SelectRequiredErr", ErrorMessageResourceType = typeof(SharedResource))]
        public string Recipient { get; set; }

        [Display(Name = "Label_CentreEmail", ResourceType = typeof(StringResource))]
        [Required(ErrorMessageResourceName = "FormItem_FieldRequiredErr", ErrorMessageResourceType = typeof(SharedResource))]
        [EmailAddress(ErrorMessageResourceName = "FormItem_EmailInvalidErr", ErrorMessageResourceType = typeof(SharedResource))]
        [StringLength(150, ErrorMessageResourceName = "FormItem_EmailStringLengthErr", ErrorMessageResourceType = typeof(SharedResource))]
        public string CentreEmail { get; set; }

        [Display(Name = "Label_CentreName", ResourceType = typeof(StringResource))]
        [StringLength(150, ErrorMessageResourceName = "FormItem_NameStringLengthErr", ErrorMessageResourceType = typeof(SharedResource))]
        public string CentreName { get; set; }

        [Display(Name = "Label_CentreId", ResourceType = typeof(StringResource))]
        [StringLength(150, ErrorMessageResourceName = "FormItem_IDStringLengthErr", ErrorMessageResourceType = typeof(SharedResource))]
        public string CentreId { get; set; }

        public virtual IList<SelectListItem> Consultants { get; set; }

        [DataType(DataType.Upload)]
        public List<IFormFile> Files { get; set; }

        [Range(1, int.MaxValue, ErrorMessageResourceName = "Upload_RequiredErr", ErrorMessageResourceType = typeof(SharedResource))]
        public int NumberOfUploadedFiles { get { return UploadedFiles?.Count ?? 0; } }

        public Dictionary<string, string> UploadedFiles { get; set; }

        public IList<SelectListItem> Recipients()
        {
            IList<SelectListItem> names = new List<SelectListItem>()
            {
                new SelectListItem{Value="QIG Reporting", Text="QIG Reporting" },
                new SelectListItem{Value="Review/Monitor", Text="Review/Monitor" }
            };
            
            return names.OrderBy(x => x.Value).ToList();
        }

        public FormItem[] GetFormItems()
        {
            return new FormItem[]
            {
                new FormItem("Centre Email", CentreEmail),
                new FormItem("Centre Name",CentreName),
                new FormItem("Centre Id",CentreId),
                new FormItem("Recipient",Recipient)
            };
        }

        public void SetFormItems(FormItem[] formItems)
        {
            var formItemList = formItems.ToList<FormItem>();

            CentreEmail = formItemList.SingleOrDefault(x => x.Name == "Centre Email")?.Value;
            CentreName = formItemList.SingleOrDefault(x => x.Name == "Centre Name")?.Value;
            CentreId = formItemList.SingleOrDefault(x => x.Name == "Centre Id")?.Value;
            Recipient = formItemList.SingleOrDefault(x => x.Name == "Recipient")?.Value;
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
                new NotifyEmailAddress(CentreEmail)
            };
        }


    }
}
