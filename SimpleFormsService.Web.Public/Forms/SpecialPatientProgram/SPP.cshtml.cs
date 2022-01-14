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

        public SPPFormModel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [BindProperty]
        public SPPForm SPPForm { get; set; }
        
        public IActionResult OnGet()
        {
            SPPForm = new SPPForm { TemplateId = _templateId };

            return Page();
        }

        public async Task<IActionResult> OnPostUploadFileAsync()
        {
            //INIT: if SbbmissionId is empty
            if (String.IsNullOrEmpty(SPPForm.SubmissionId))
            {
                var formSub = await _serviceManager.FormSubmissionService.Init(SPPForm.TemplateId);
                SPPForm.SubmissionId = formSub.Id.ToString();
            }

            //UPLOAD FILE: if Files is not null (ignore form fields)
            if (SPPForm.Files != null && SPPForm.Files.Count == 1)
            {
                ModelState.ClearValidationState("SPPForm.NumberOfUploadedFiles");
                if (SPPForm.Files[0].Length > 10485760)
                {
                    ModelState.AddModelError("SPPForm.NumberOfUploadedFiles", StringResource.Upload_FileSizeErr);
                }
                else if (SPPForm.UploadedFiles != null && SPPForm.UploadedFiles.Any(x => x.Value == SPPForm.Files[0].FileName))
                {
                    ModelState.AddModelError("SPPForm.NumberOfUploadedFiles", StringResource.Upload_DuplicateErr);
                }
                else
                {
                    var formSub = await _serviceManager.FormSubmissionService.UploadFile(SPPForm.TemplateId, SPPForm.SubmissionId, SPPForm.Files[0]);
                    SPPForm.SetFormFiles(formSub.Data.DocumentReferences);
                }                
            }

            return Page();
        }

        public async Task<IActionResult> OnPostRemoveFileAsync()
        {            
            var formSub = await _serviceManager.FormSubmissionService.DeleteFile(SPPForm.TemplateId, SPPForm.SubmissionId, SPPForm.FileIdToDelete);
            SPPForm.SetFormFiles(formSub.Data.DocumentReferences);
            SPPForm.FileIdToDelete = null;

            return Page();
        }

        public async Task<IActionResult> OnPostSubmitFormAsync()
        {
            //INIT: if SbbmissionId is empty
            if (String.IsNullOrEmpty(SPPForm.SubmissionId))
            {
                var formSub = await _serviceManager.FormSubmissionService.Init(SPPForm.TemplateId);
                SPPForm.SubmissionId = formSub.Id.ToString();
            }

            //UPLOAD FILE: if Files is not null (ignore form fields)
            //if (SPPForm.Files != null && SPPForm.Files.Count == 1)
            //{
            //    var formSub = await _serviceManager.FormSubmissionService.UploadFile(SPPForm.TemplateId, SPPForm.SubmissionId, SPPForm.Files[0]);
            //    SPPForm.SetFormFiles(formSub.Data.DocumentReferences);

            //    if (SPPForm.UploadedFiles.Count > 0) ModelState.ClearValidationState("SPPForm.UploadedFiles");

            //    return Page();
            //}

            

            //SUBMIT FORM: If ModelState IsValid, Data.FormItems can be saved
            if (ModelState.IsValid)
            {
                var formSub = await _serviceManager.FormSubmissionService.GetFormSubmissionByIdAsync(SPPForm.SubmissionId);
                var formData = formSub.Data;
                formData.FormItems = SPPForm.GetFormItems();

                formSub = await _serviceManager.FormSubmissionService.SubmitForm(formSub.TemplateId.ToString(), formSub.Id.ToString(), formData);

                if (formSub != null) return RedirectToPage("/Submission/Confirmation");
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
