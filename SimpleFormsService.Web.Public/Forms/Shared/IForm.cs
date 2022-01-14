using Microsoft.AspNetCore.Http;
using SimpleFormsService.Domain.Entities.Supporting.JSON;
using System.Collections.Generic;

namespace SimpleFormsService.Web.Public.Forms.Shared
{
    public interface IForm
    {
        string SubmissionId { get; set; }
        string TemplateId { get; set; }
        List<IFormFile> Files { get; set; }
        FormItem[] GetFormItems();
        void SetFormItems(FormItem[] formItems);
        DocumentReference[] GetFormFiles();
        void SetFormFiles(DocumentReference[] documentReferences);
    }
}