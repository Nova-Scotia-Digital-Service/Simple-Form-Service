using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace SimpleFormsService.Web.Public.Forms.DrugInformationSystem
{
    [ValidateAntiForgeryToken(Order = 1000)]
    public class DISPharmacyModel : PageModel
    {
        [BindProperty]
        public DISPharmacy DISPharmacy { get; set; }
        public DISPharmacyModel()
        {
        }

        public void OnGet()
        {
        }

        public List<SelectListItem> SourceTypes()
        {
            List<SelectListItem> names = new()
            {
                new SelectListItem { Value = StringResource.Label_Pharmacy, Text = "Pharmacy" },
                new SelectListItem { Value = StringResource.Label_Vendor, Text = "Vendor" },
                new SelectListItem { Value = StringResource.Label_Org, Text = "Organization" }
            };

            return names.OrderBy(x => x.Value).ToList();
        }
    }
}
