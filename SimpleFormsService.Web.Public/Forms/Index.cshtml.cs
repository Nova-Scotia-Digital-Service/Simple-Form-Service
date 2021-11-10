using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace SimpleFormsService.Web.Public.Forms
{
    [ValidateAntiForgeryToken(Order = 1000)]
    public class IndexModel : PageModel
    {      
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
