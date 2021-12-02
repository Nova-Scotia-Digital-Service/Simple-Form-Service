using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SimpleFormsService.Web.Admin.Pages
{
    [ValidateAntiForgeryToken(Order = 1000)]
    public class IndexModel : PageModel
    {
        [BindProperty]
        public FormInfo FormInfo { get; set; }
        public IndexModel()
        {
        }

        public void OnGet()
        {

        }
    }
}