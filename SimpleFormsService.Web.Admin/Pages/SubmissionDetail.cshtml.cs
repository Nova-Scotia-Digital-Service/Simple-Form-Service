using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleFormsService.Web.Admin.Models;

namespace SimpleFormsService.Web.Admin.Pages
{
    [ValidateAntiForgeryToken(Order = 1000)]
    public class SubmissionDetailModel : PageModel
    {
        [BindProperty]
        public FormInfo FormInfo { get; set; }
        
        public SubmissionDetailModel()
        {
           
        }

        public void OnGet()
        {
           
        }

    }
}