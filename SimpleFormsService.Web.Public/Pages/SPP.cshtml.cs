﻿using SimpleFormsService.Web.Public.Models; 
using SimpleFormsService.Web.Public.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleFormsService.Web.Public.Pages
{
    [ValidateAntiForgeryToken(Order = 1000)]
    public class SPPFormModel : PageModel
    {
        [BindProperty]
        public SPPForm SPPForm { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("./redirectURL");
        }
        public List<SelectListItem> SubmissionTypes()
        {
            List<SelectListItem> items = new()
            {
                new SelectListItem { Text = StringResource_SPP.SubmissionType_New, Value = "new" },
                new SelectListItem { Text = StringResource_SPP.SubmissionType_Existing, Value = "existing" }
            };

            return items.OrderBy(x => x.Text).ToList();
        }
    }
}
