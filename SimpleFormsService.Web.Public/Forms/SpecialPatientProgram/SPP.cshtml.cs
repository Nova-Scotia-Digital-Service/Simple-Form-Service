using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleFormsService.Domain.Entities.Supporting;
using SimpleFormsService.Domain.Entities.Supporting.JSON;
using SimpleFormsService.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleFormsService.Web.Public.Forms.SpecialPatientProgram
{
    [ValidateAntiForgeryToken(Order = 1000)]
    public class SPPFormModel : PageModel
    {
        private readonly IServiceManager _serviceManager;
        
        //KDA - these are configurable values that should eventually come from the Form Template
        private const string _templateId = "a7b65d0f-5b87-4050-a5ef-ef79ef0ec753";
        private NotifyEmailAddress[] _notifyEmailAddresses = {  };

        public SPPFormModel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [BindProperty]
        public SPPForm SPPForm { get; set; }
        
        public IActionResult OnGet()
        {            
;            return Page();
        }
                
        public async Task<IActionResult> OnPostAsync()
        {
            if(string.IsNullOrEmpty(SPPForm.SubmissionId))
            {
                var formSubmission = await _serviceManager.FormSubmissionService.Init(_templateId);
                if (formSubmission != null) SPPForm.SubmissionId = formSubmission.Id.ToString();
            }
            
            if(SPPForm.Files != null)
            {
                var uploadedFiles = await _serviceManager.MinIoDocumentService.UploadFiles(SPPForm.Files, _templateId);

                if (SPPForm.UploadedFiles == null) SPPForm.UploadedFiles = new Dictionary<string, string>();

                for (int i = 0; i < uploadedFiles.Count; i++)
                {
                    SPPForm.UploadedFiles.Add(uploadedFiles[i],SPPForm.Files[i].FileName);
                }

                if (SPPForm.UploadedFiles.Count > 0) ModelState.ClearValidationState("SPPForm.UploadedFiles");

                return Page();
            }

            if (ModelState.IsValid)
            {
                var docRefs = new DocumentReference[] { };
                if (SPPForm.UploadedFiles != null)
                {
                    docRefs = SPPForm.UploadedFiles.Select(x => new DocumentReference(_templateId, x.Key)).ToArray();
                }

                if (!string.IsNullOrEmpty(SPPForm.SubmissionId))
                {
                    var formSubmission = _serviceManager.FormSubmissionService.SubmitForm(
                        _templateId,
                        SPPForm.SubmissionId,
                        new FormSubmissionData(
                            SPPForm.SubmissionId,
                            _templateId,
                            DateTime.Now.ToString(),
                            FormSubmissionStatus.Submitted.ToString(),
                            DateTime.Now.ToString(),
                            User.Identity.Name,
                            DateTime.Now.ToString(),
                            User.Identity.Name,
                            _notifyEmailAddresses,
                            SPPForm.GetFormItems(),
                            docRefs
                            )
                        ); 
                    
                    if(formSubmission != null) return RedirectToPage("/Submission/Confirmation");
                }
                                
            }
            return Page();
        }

        public List<SelectListItem> SubmissionTypes()
        {
            List<SelectListItem> items = new()
            {
                new SelectListItem { Text = StringResource.SubmissionType_New, Value = "new" },
                new SelectListItem { Text = StringResource.SubmissionType_Existing, Value = "existing" }
            };

            return items.OrderBy(x => x.Text).ToList();
        }        
    }
}
