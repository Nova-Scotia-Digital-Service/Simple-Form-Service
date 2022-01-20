using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleFormsService.Services.Abstractions;
using SimpleFormsService.Web.Admin.Models.Admin;

namespace SimpleFormsService.Web.Admin.Controllers.Admin
{
    public class AdminController : Controller
    {
        private readonly IServiceManager _serviceManager;
        public AdminController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //Enable search page post MVP
            return RedirectToAction("Error", "Error");
            //return View();
        }

        [HttpGet]
        [Authorize(Policy = "GroupAdmin")]
        [Route("Admin/Submission-Detail/{templateId}/{submissionId}")]
        public async Task<IActionResult> SubmissionDetail(string templateId, string submissionId, CancellationToken cancellationToken)
        {         
            SubmissionDetailModel detail = new();

            try
            {
                detail.FormSubmission = await _serviceManager.FormSubmissionService.GetFormSubmissionByIdTemplateIdAsync(submissionId, templateId, cancellationToken);
                if (detail.FormSubmission != null)
                {
                    detail.FormItems = detail.FormSubmission.Data.FormItems;
                    detail.Documents = detail.FormSubmission.Data.DocumentReferences;
                    detail.SubmissionData = detail.FormSubmission.Data;
                }
                else
                {
                    Console.WriteLine("===== INFO: Admin UI Form submission is empty. ======");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("====== ERROR: Unable to render submission detail ====== " + ex.StackTrace);
            }

            return View(detail);
        }

        [HttpGet]
        [Route("Admin/View-Document/{templateId}/{documentId}")]
        public async Task<IActionResult> ViewDocument(string templateId, string documentId, CancellationToken cancellationToken)
        {
            var fileStreamResultAdapter = await _serviceManager.MinIoDocumentService.GetObject(templateId, documentId, cancellationToken);
            
            return File(fileStreamResultAdapter.MemoryStream, fileStreamResultAdapter.ContentType);
        }
    }
}
