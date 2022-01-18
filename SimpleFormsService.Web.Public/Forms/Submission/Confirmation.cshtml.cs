using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SimpleFormsService.Domain.Entities.Supporting.JSON;

namespace SimpleFormsService.Web.Public.Forms.Submission
{
    public class ConfirmationModel : PageModel
    {
        public FormSubmissionData FormData { get; set; }
        public IActionResult OnGet()
        {
            if (TempData["ConfirmationFormData"] == null)
            {
                return RedirectToPage("~/");
            }
            else
            {
                FormData = JsonConvert.DeserializeObject<FormSubmissionData>(TempData["ConfirmationFormData"].ToString());
                TempData.Clear();
            }

            return Page();
        }
    }
}
