using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using SimpleFormsService.Web.Public.Forms.Shared;

namespace SimpleFormsService.Web.Public.Forms.ChildCareSubsidy
{
    [ValidateAntiForgeryToken(Order = 1000)]
    public class CCSFormModel : PageModel
    {
        [BindProperty]
        public CCSForm CCSForm { get; set; }
        public CCSFormModel()
        {
        }

        public void OnGet()
        {
        }

        public List<SelectListItem> RegionalOffices()
        {
            List<SelectListItem> names = new()
            {
                new SelectListItem { Value = StringResource.CCSOffice_Kentville, Text="Kentville" },
                new SelectListItem { Value = StringResource.CCSOffice_Halifax, Text = "Halifax" },
                new SelectListItem { Value = StringResource.CCSOffice_Sydney, Text="Sydney" },
                new SelectListItem { Value = StringResource.CCSOffice_Truro, Text="Truro" },
                new SelectListItem { Value = StringResource.CCSOffice_Yarmouth, Text="Yarmouth" }
            };
            
            return names.OrderBy(x => x.Value).ToList();
        }

        public List<ListItem> SupportDocuments()
        {
            List<ListItem> documents = new()
            {
                new ListItem { Description = StringResource.CCSFormat_Paper, Code = "P", Hint = StringResource.CCSDelivery_Paper },
                new ListItem { Description = StringResource.CCSFormat_Digital, Code = "D", Hint = StringResource.CCSDelivery_Digital },
                new ListItem { Description = StringResource.CCSFormat_Both, Code = "Both", Hint = StringResource.CCSDelivery_Both },
            };

            return documents;
        }
    }
}
