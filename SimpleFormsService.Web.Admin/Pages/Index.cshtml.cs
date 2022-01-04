using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleFormsService.Web.Admin.Models;

namespace SimpleFormsService.Web.Admin.Pages
{
    [ValidateAntiForgeryToken(Order = 1000)]
    public class IndexModel : PageModel
    {
        [BindProperty]
        public SearchSubmission SearchSubmission { get; set; }
        public IndexModel()
        {

        }

        public void OnGet()
        {
            var test = User.Identity;
        }
    }
}