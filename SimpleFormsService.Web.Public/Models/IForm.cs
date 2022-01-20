using Microsoft.AspNetCore.Http;
using SimpleFormsService.Domain.Entities.Supporting.JSON;
using System.Collections.Generic;

namespace SimpleFormsService.Web.Public.Models
{
    public interface IForm
    {
        string SubmissionId { get; set; }
        string TemplateId { get; set; }
        string FileIdToDelete { get; set; }

        List<IFormFile> Files { get; set; }
        int NumberOfUploadedFiles { get; }
        Dictionary<string, string> UploadedFiles { get; set; }
        string FormCode { get; set; }
        public string FormTitle { get; set; }

        FormItem[] GetFormItems();
        void SetFormItems(FormItem[] formItems);
        DocumentReference[] GetFormFiles();
        void SetFormFiles(DocumentReference[] documentReferences);
        NotifyEmailAddress[] GetConfirmationEmailAddresses();
    }
}