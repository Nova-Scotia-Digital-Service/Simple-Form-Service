using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace SimpleFormsService.Web.Public.Forms.EarlyChildhoodDevelopmentService
{
    [ValidateAntiForgeryToken(Order = 1000)]
    public class ECDSGrantsModel : PageModel
    {
        [BindProperty]
        public ECDSGrant ECDSGrant { get; set; }
        public ECDSGrantsModel()
        {
        }

        public void OnGet()
        {
        }

        public IList<SelectListItem> Consultants()
        {
            IList<SelectListItem> names = new List<SelectListItem>()
            {
                new SelectListItem{Value="John Doe", Text="John Doe" },
                new SelectListItem{Value="Jane Doe", Text="Jane Doe" }
            };

            return names;
        }
    }
}
