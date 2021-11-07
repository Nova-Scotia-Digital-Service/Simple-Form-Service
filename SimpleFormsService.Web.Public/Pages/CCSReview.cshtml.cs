using SimpleFormsService.Web.Public.Models; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace SimpleFormsService.Web.Public.Pages
{
    [ValidateAntiForgeryToken(Order = 1000)]
    public class CCSReviewModel : PageModel
    {
        [BindProperty]
        public CCSReview CCSReview { get; set; }
        public CCSReviewModel()
        {
        }

        public void OnGet()
        {
        }

        public IList<SelectListItem> CaseWorkers()
        {
            IList<SelectListItem> names = new List<SelectListItem>()
            {
                new SelectListItem{Value="Jane Doe", Text="Jane Doe" },
                new SelectListItem{Value="John Doe", Text="John Doe" },
                new SelectListItem{Value="John Smith", Text="John Smith" }
            };

            return names;
        }
    }
}
