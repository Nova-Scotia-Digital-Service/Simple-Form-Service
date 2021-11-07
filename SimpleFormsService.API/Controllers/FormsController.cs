using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SimpleFormsService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormsController : ControllerBase
    {

       // app.MapPost("/api/public/{form-id}/{submission-id}/upload-file") 
        //app.MapPost("/api/public/{form-id}/{submission-id}/submit-form") 
        

        // POST api/<FormsController>
        [HttpPost("api/forms/{formId}/{submissionId}/upload-file")]
        public void UploadFile(string formId, string submissionId)
        {
            //TODO: do something
            Console.WriteLine("FormID: {0}, SubmissionID: {1}",formId, submissionId);
        }


        [HttpPost("api/forms/{formId}/{submissionId}/submit-form")]
        public void SubmitForm(string formId, string submissionId, [FromBody] string formData)
        {
            //TODO: do something
            Console.WriteLine("FormID: {0}, SubmissionID: {1}", formId, submissionId);
        }

    }
}
