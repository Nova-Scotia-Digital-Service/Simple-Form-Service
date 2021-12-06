using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SimpleFormsService.Web.Public.Forms.Submission
{
    public class ConfirmationModel : PageModel
    {
        public string SubmissionID { get; set; } = "12345";
        public void OnGet()
        {
            
        }
    }
}
