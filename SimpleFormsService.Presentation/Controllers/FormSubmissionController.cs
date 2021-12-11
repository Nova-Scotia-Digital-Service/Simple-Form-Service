using Microsoft.AspNetCore.Mvc;
using SimpleFormsService.Services.Abstractions;

namespace SimpleFormsService.Presentation.Controllers
{
    [ApiController]
    [Route("submission/")]
    public class FormSubmissionController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public FormSubmissionController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFormSubmissionById(string id, CancellationToken cancellationToken)
        {
            var formSubmissionDto = await _serviceManager.FormSubmissionService.GetFormSubmissionByIdAsync(id, cancellationToken);
            return Ok(formSubmissionDto);
        }

        [HttpGet("{templateId}")]
        public async Task<IActionResult> GetFormSubmissionsByTemplateId(string templateId, CancellationToken cancellationToken)
        {
            var testSummaryDtos = await _serviceManager.FormSubmissionService.GetFormSubmissionsByTemplateIdAsync(templateId, cancellationToken);
            return Ok(testSummaryDtos);
        }
    }
}