using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleFormsService.Services.Abstractions;
using SimpleFormsService.Web.Admin.Models;

namespace SimpleFormsService.Web.Admin.Pages
{
    [ValidateAntiForgeryToken(Order = 1000)]
    public class IndexModel : PageModel
    {
        private readonly IServiceManager _serviceManager;

        [BindProperty]
        public SearchSubmission SearchSubmission { get; set; }
        
        public IndexModel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public void OnGet()
        {
            var test = User.Identity;

            var hasAccess = _serviceManager.FormTemplateSecurityService.HasAccess("6979c133-852a-48ed-951b-81d8fe5f6b99").Result;
        }
    }
}