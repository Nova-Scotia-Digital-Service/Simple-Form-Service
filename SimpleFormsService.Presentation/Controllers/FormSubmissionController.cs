﻿// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using SimpleFormsService.Domain.Entities.Supporting.JSON;
// using SimpleFormsService.Services.Abstractions;
//
// namespace SimpleFormsService.Presentation.Controllers
// {
//     [ApiController]
//     [Route("api/public/")]
//     public class FormSubmissionController : ControllerBase
//     {
//         private readonly IServiceManager _serviceManager;
//
//         public FormSubmissionController(IServiceManager serviceManager) => _serviceManager = serviceManager;
//
//         [HttpPost("{templateId}/init")]
//         public async Task<IActionResult> Init(string templateId, CancellationToken cancellationToken)
//         {
//             var formSubmission = await _serviceManager.FormSubmissionService.Init(templateId, cancellationToken);
//
//             return Ok(formSubmission);
//         }
//
//         [HttpPost("{templateId}/{submissionId}/upload-file")]
//         public async Task<IActionResult> UploadFile(string templateId, string submissionId, IFormFile file, CancellationToken cancellationToken)
//         {
//             var formSubmission = await _serviceManager.FormSubmissionService.UploadFile(templateId, submissionId, file, cancellationToken);
//
//             return Ok(formSubmission);
//         }
//
//         [HttpPost("{templateId}/{submissionId}/submit-form")]
//         public async Task<IActionResult> SubmitForm(string templateId, string submissionId, FormSubmissionData data, CancellationToken cancellationToken)
//         {
//             var formSubmission = await _serviceManager.FormSubmissionService.SubmitForm(templateId, submissionId, data, cancellationToken);
//
//             return Ok(formSubmission);
//         }
//     }
// }