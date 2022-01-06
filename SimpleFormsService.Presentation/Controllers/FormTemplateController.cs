using Microsoft.AspNetCore.Mvc;
using SimpleFormsService.Services.Abstractions;

namespace SimpleFormsService.Presentation.Controllers
{
    [ApiController]
    [Route("template/")]
    public class FormTemplateController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public FormTemplateController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFormTemplateById(string id, CancellationToken cancellationToken)
        {
            var formTemplateDto = await _serviceManager.FormTemplateService.GetFormTemplateByIdAsync(id, cancellationToken);
            return Ok(formTemplateDto);
        }

        /*
        [HttpGet("api/admin/{templateId}/{submissionId}")]
        public string ViewForm(string templateId, string submissionId)
        {
            return "Form Data";
        }

        [HttpGet("api/admin/{templateId}/{submissionId}/view-document/{documentId}")]
        public string ViewForm(string templateId, string submissionId, string documentId)
        {
            return "Document Data";
        }

        // todo get file 

        //app.MapPost("/api/config/{template-id}) //create a new form configuration
        //app.MapGet("/api/config/{template-id}) // get the config for the specified form
        //app.MapPatch("/api/config/{template-id}) //update the current form's configuration.  Ensure not to change the id (obviously)

        */
    }
}