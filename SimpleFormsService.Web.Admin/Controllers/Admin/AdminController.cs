﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minio.DataModel;
using SimpleFormsService.Domain.Entities.Supporting.JSON;
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
            return View();
        }

        [HttpGet]
        [Authorize(Policy = "GroupAdmin")]
        [Route("Admin/Submission-Detail/{templateId}/{submissionId}")]
        public async Task<IActionResult> SubmissionDetail(string templateId, string submissionId, CancellationToken cancellationToken)
        {         
            SubmissionDetailModel detail = new();
            
            detail.FormSubmission = await _serviceManager.FormSubmissionService.GetFormSubmissionByIdAsync(submissionId, cancellationToken);
            if (detail.FormSubmission != null)
            {
                detail.FormItems = detail.FormSubmission.Data.FormItems;
                detail.Documents = detail.FormSubmission.Data.DocumentReferences;
                detail.SubmissionData = detail.FormSubmission.Data;
            }
            else
            {
                Console.WriteLine("===== INFO: Form submission is empty. ======");
            }

            return View(detail);
        }


        [HttpGet]
        [Route("Admin/View-Document/{templateId}/{documentId}")]
        public async Task<IActionResult> ViewDocument(string templateId, string documentId, CancellationToken cancellationToken)
        {          
            ObjectStat objectStat = await _serviceManager.MinIoDocumentService.FindObject(templateId, documentId, cancellationToken);
            MemoryStream responseStream = await _serviceManager.MinIoDocumentService.GetObject(templateId, documentId, cancellationToken);

            return File(responseStream, objectStat.ContentType);
        }

    }
}