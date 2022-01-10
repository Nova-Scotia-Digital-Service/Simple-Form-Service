using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleFormsService.Domain.Entities;
using SimpleFormsService.Services.Abstractions;
using SimpleFormsService.Web.Admin.Models;

namespace SimpleFormsService.Web.Admin.Pages
{
    [Authorize(Policy = "GroupAdmin")]
    [ValidateAntiForgeryToken(Order = 1000)]
    public class SubmissionDetailModel : PageModel
    {
        private readonly IServiceManager _serviceManager;
        public string TemplateId { get; set; }
        public string SubmissionId { get; set; }

        [BindProperty]
        public FormInfo FormInfo { get; set; }

        public SubmissionDetailModel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            FormInfo = new();
        }

        public async Task OnGet(string templateId, string submissionId, CancellationToken cancellationToken)
        {
            //assuming is authenticated
            FormSubmission formSubmission = await _serviceManager.FormSubmissionService.GetFormSubmissionByIdAsync(submissionId, cancellationToken);
            FormInfo.FormItems = formSubmission.Data.FormItems;
            FormInfo.Documents = formSubmission.Data.DocumentReferences;
            FormInfo.SubmissionData = formSubmission.Data;
            
            //TODO: decide if they need to be removed later on
            TemplateId = templateId;
            SubmissionId = submissionId;
        }
    }
}