using Microsoft.AspNetCore.Mvc;
using SimpleFormsService.Services.Abstractions.Application;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SimpleFormsService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public AdminController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet ("api/admin/{templateId}/{submissionId}")]
        public string ViewForm(string templateId, string submissionId)
        {
            return "Form Data";
        }

        [HttpGet("api/admin/{templateId}/{submissionId}/view-document/{documentId}")]
        public string ViewForm(string templateId, string submissionId, string documentId)
        {
            return "Document Data";
        }

        //app.MapPost("/api/config/{template-id}) //create a new form configuration
        //app.MapGet("/api/config/{template-id}) // get the config for the specified form
        //app.MapPatch("/api/config/{template-id}) //update the current form's configuration.  Ensure not to change the id (obviously)
    }
}
