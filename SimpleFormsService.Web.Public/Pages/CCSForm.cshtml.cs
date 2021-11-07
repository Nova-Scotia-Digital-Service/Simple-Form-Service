using SimpleFormsService.Web.Public.Models;
using SimpleFormsService.Web.Public.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace SimpleFormsService.Web.Public.Pages
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
                new SelectListItem { Value = StringResource_CCSForm.CCSOffice_Kentville, Text="Kentville" },
                new SelectListItem { Value = StringResource_CCSForm.CCSOffice_Halifax, Text = "Halifax" },
                new SelectListItem { Value = StringResource_CCSForm.CCSOffice_Sydney, Text="Sydney" },
                new SelectListItem { Value = StringResource_CCSForm.CCSOffice_Truro, Text="Truro" },
                new SelectListItem { Value = StringResource_CCSForm.CCSOffice_Yarmouth, Text="Yarmouth" }
            };
            
            return names.OrderBy(x => x.Value).ToList();
        }

        public List<ListItem> SupportDocuments()
        {
            List<ListItem> documents = new()
            {
                new ListItem { Description = StringResource_CCSForm.CCSFormat_Paper, Code = "P", Hint = StringResource_CCSForm.CCSDelivery_Paper },
                new ListItem { Description = StringResource_CCSForm.CCSFormat_Digital, Code = "D", Hint = StringResource_CCSForm.CCSDelivery_Digital },
                new ListItem { Description = StringResource_CCSForm.CCSFormat_Both, Code = "Both", Hint = StringResource_CCSForm.CCSDelivery_Both },
            };

            return documents;
        }
    }
}
