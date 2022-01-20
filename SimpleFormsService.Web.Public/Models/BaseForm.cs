using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleFormsService.Domain.Entities.Supporting.JSON;
using SimpleFormsService.Web.Public.Forms.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SimpleFormsService.Web.Public.Views.Shared;
using SimpleFormsService.Web.Public.Views.Forms.SpecialPatientProgram;

namespace SimpleFormsService.Web.Public.Models
{
    public class BaseForm
    {
        public string SubmissionId { get; set; }
        public string TemplateId { get; set; }
        public string FileIdToDelete { get; set; }
        public string FormCode { get; set; }
        public string FormTitle { get; set; }
    }
}
