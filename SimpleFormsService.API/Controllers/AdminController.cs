using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SimpleFormsService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        [HttpGet ("api/admin/{formId}/{submissionId}")]
        public string ViewForm(string formId, string submissionId)
        {
            return "Form Data";
        }

        [HttpGet("api/admin/{formId}/{submissionId}/view-document/{documentId}")]
        public string ViewForm(string formId, string submissionId, string documentId)
        {
            return "Document Data";
        }

        //app.MapPost("/api/config/{form-id}) //create a new form configuration
        //app.MapGet("/api/config/{form-id}) // get the config for the specified form
        //app.MapPatch("/api/config/{form-id}) //update the current form's configuration.  Ensure not to change the id (obviously)

    }
}
