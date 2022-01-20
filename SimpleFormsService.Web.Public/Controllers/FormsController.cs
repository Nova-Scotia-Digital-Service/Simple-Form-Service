using Microsoft.AspNetCore.Mvc;
using SimpleFormsService.Services.Abstractions;
using SimpleFormsService.Web.Public.Views.Forms.SpecialPatientProgram;
using SimpleFormsService.Web.Public.Views.Shared;
using SimpleFormsService.Web.Public.Models;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using SimpleFormsService.Domain.Entities;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using SimpleFormsService.Domain.Entities.Supporting.JSON;

namespace SimpleFormsService.Web.Public.Controllers
{
    public class FormsController : Controller
    {
        private readonly IServiceManager _serviceManager;

        private readonly Dictionary<string, string[]> FormTemplates = new Dictionary<string, string[]>();

        //KDA - temporary collection of FormTemplates until security is resolved to obtain collection from DB
        public FormsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            //KDA - temporary collection of FormTemplates until security is resolved to obtain collection from DB
            FormTemplates.Add("a7b65d0f-5b87-4050-a5ef-ef79ef0ec753", new string[] { "SPPForm", "Special Patient Program" });
            FormTemplates.Add("170e324d-7aa9-4faf-8b42-5a7f6363fda5", new string[] { "ECDSGrant", "Early Childhood Development Service" });
            FormTemplates.Add("17fcd5c7-84cf-4cdc-bb2f-5649396ae8c6", new string[] { "DISPharmacy", "Drug Information System" });
            FormTemplates.Add("0d9fe6dc-a167-4fe1-9de4-dac996a44ac8", new string[] { "CCSReview", "Child Care Subsidy Review" });
            FormTemplates.Add("a84c16c0-5ea6-4cc0-87ae-2fdd575715e0", new string[] { "CCSForm", "Child Care Subsidy" });
        }

        [HttpGet("NewForms")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("NewSubmission/{templateId}")]
        public IActionResult NewSubmission(string templateId)
        {
            var formCode = FormTemplates[templateId][0];
            var formTitle = FormTemplates[templateId][1];
            
            var type = Type.GetType("SimpleFormsService.Web.Public.Models." + formCode);
            
            var form = (IForm)Activator.CreateInstance(type);
            form.TemplateId = templateId;
            form.FormCode = formCode;
            form.FormTitle = formTitle;        

            return View(form);
        }

        //[HttpPost("NewSubmission/{templateId}")]
        //public async Task<IActionResult> SubmitForm(IForm form, string handler)

        //KDA - need to solve generic model binding to unpack form items; until then, each form type needs a unique action
        [HttpPost("NewSubmission/a7b65d0f-5b87-4050-a5ef-ef79ef0ec753")]
        public async Task<IActionResult> SubmitFormSPP(SPPForm form, string handler)
        {
            return await submitForm(form, handler);
        }

        [HttpPost("NewSubmission/170e324d-7aa9-4faf-8b42-5a7f6363fda5")]
        public async Task<IActionResult> SubmitFormECDSGrants(ECDSGrant form, string handler)
        {
            return await submitForm(form, handler);
        }

        [HttpPost("NewSubmission/17fcd5c7-84cf-4cdc-bb2f-5649396ae8c6")]
        public async Task<IActionResult> SubmitFormDISPharmacy(DISPharmacy form, string handler)
        {
            return await submitForm(form, handler);
        }

        [HttpPost("NewSubmission/0d9fe6dc-a167-4fe1-9de4-dac996a44ac8")]
        public async Task<IActionResult> SubmitFormCCSReview(CCSReview form, string handler)
        {
            return await submitForm(form, handler);
        }

        [HttpPost("NewSubmission/a84c16c0-5ea6-4cc0-87ae-2fdd575715e0")]
        public async Task<IActionResult> SubmitFormCCSForm(CCSForm form, string handler)
        {
            return await submitForm(form, handler);
        }

        private async Task<IActionResult> submitForm(IForm form, string handler)
        {
            if(handler == "UploadFile")
            {
                //INIT: if SbbmissionId is empty
                if (String.IsNullOrEmpty(form.SubmissionId))
                {
                    var formSub = await _serviceManager.FormSubmissionService.Init(form.TemplateId);
                    form.SubmissionId = formSub.Id.ToString();
                }

                //UPLOAD FILE: if Files is not null (ignore form fields)
                if (form.Files != null && form.Files.Count == 1)
                {
                    ModelState.ClearValidationState("NumberOfUploadedFiles");
                    if (!SharedResource.Upload_AllowedTypes.Contains(Path.GetExtension(form.Files[0].FileName)))
                    {
                        ModelState.AddModelError("NumberOfUploadedFiles", SharedResource.Upload_FileTypeErr);
                    }
                    else if (form.Files[0].Length > 10485760)
                    {
                        ModelState.AddModelError("NumberOfUploadedFiles", SharedResource.Upload_FileSizeErr);
                    }
                    else if (form.UploadedFiles != null && form.UploadedFiles.Any(x => x.Value == form.Files[0].FileName))
                    {
                        ModelState.AddModelError("NumberOfUploadedFiles", SharedResource.Upload_DuplicateErr);
                    }
                    else
                    {
                        var formSub = await _serviceManager.FormSubmissionService.UploadFile(form.TemplateId, form.SubmissionId, form.Files[0]);
                        form.SetFormFiles(formSub.Data.DocumentReferences);
                    }
                }
            } 
            else if(handler == "RemoveFile")
            {
                var formSub = await _serviceManager.FormSubmissionService.DeleteFile(form.TemplateId, form.SubmissionId, form.FileIdToDelete);
                form.SetFormFiles(formSub.Data.DocumentReferences);
                form.FileIdToDelete = null;
            } 
            else
            {
                //INIT: if SubmissionId is empty
                if (String.IsNullOrEmpty(form.SubmissionId))
                {
                    var formSub = await _serviceManager.FormSubmissionService.Init(form.TemplateId);
                    form.SubmissionId = formSub.Id.ToString();
                }

                //SUBMIT FORM: If ModelState IsValid, Data.FormItems can be saved
                if (ModelState.IsValid)
                {
                    var formSub = await _serviceManager.FormSubmissionService.GetFormSubmissionByIdAsync(form.SubmissionId);
                    var formData = formSub.Data;
                    formData.FormItems = form.GetFormItems();
                    formData.ConfirmationEmailAddresses = form.GetConfirmationEmailAddresses();

                    formSub = await _serviceManager.FormSubmissionService.SubmitForm(formSub.TemplateId.ToString(), formSub.Id.ToString(), formData);

                    if (formSub != null)
                    {
                        TempData.Clear();
                        TempData.Add("ConfirmationFormData", JsonConvert.SerializeObject(formSub.Data));
                        return RedirectToAction("Confirmation", new { templateId = form.TemplateId });
                    }
                }
            }
            
            return View("NewSubmission", form);
        }

        [HttpGet("Confirmation/{templateId}")]
        public IActionResult Confirmation(string templateId)
        {
            if (TempData["ConfirmationFormData"] == null)
            {
                return RedirectToPage("~/");
            }
            else
            {
                var formData = JsonConvert.DeserializeObject<FormSubmissionData>(TempData["ConfirmationFormData"].ToString());
                TempData.Clear();
                return View(formData);
            }
        }


    }
}
