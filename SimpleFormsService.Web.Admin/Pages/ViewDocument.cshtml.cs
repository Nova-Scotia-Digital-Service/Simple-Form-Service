using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Minio.DataModel;
using SimpleFormsService.Services.Abstractions;

namespace SimpleFormsService.Web.Admin.Pages
{
    [ValidateAntiForgeryToken(Order = 1000)]
    public class ViewDocumentModel : PageModel
    {
        private readonly IServiceManager _serviceManager;
        public string DocumentId { get; set; }
        public string TemplateId { get; set; }

        public ViewDocumentModel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        

        public async Task<IActionResult> OnGet(CancellationToken cancellationToken)
        {
            string path = Request.Path.Value;

            if (!string.IsNullOrWhiteSpace(path))
            {
                TemplateId = path.Split(new string[] { "/" }, 4, StringSplitOptions.None)[2];
                DocumentId = path.Split(new string[] { "/" }, 4, StringSplitOptions.None)[3];
            }
            else
                Console.WriteLine("====== Error: Unable to get MINIO object ====== TemplateId is NULL? " + string.IsNullOrWhiteSpace(TemplateId) + " DocumentId is NULL?? " + string.IsNullOrWhiteSpace(DocumentId));

            ObjectStat objectStat = await _serviceManager.MinIoDocumentService.FindObject(TemplateId, DocumentId, cancellationToken);
            MemoryStream responseStream = await _serviceManager.MinIoDocumentService.GetObject(TemplateId, DocumentId, cancellationToken);

            return File(responseStream, objectStat.ContentType);
        }
    }
}