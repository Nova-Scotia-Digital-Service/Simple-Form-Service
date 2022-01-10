using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Minio;
using Minio.DataModel;
using SimpleFormsService.Configuration;

namespace SimpleFormsService.Web.Admin.Pages
{
    [ValidateAntiForgeryToken(Order = 1000)]
    public class ViewDocumentModel : PageModel
    {
        public string DocumentId { get; set; }
        public string TemplateId { get; set; }

        public ViewDocumentModel()
        {

        }
        

        public async Task<IActionResult> OnGet()
        {
            string path = this.Request.Path.Value;

            if (!string.IsNullOrWhiteSpace(path))
            {
                TemplateId = path.Split(new string[] { "/" }, 4, StringSplitOptions.None)[2];
                DocumentId = path.Split(new string[] { "/" }, 4, StringSplitOptions.None)[3];
            }
            else
                Console.WriteLine("====== Error: Unable to get MINIO object ====== TemplateId is NULL? " + string.IsNullOrWhiteSpace(TemplateId) + " DocumentId is NULL?? " + string.IsNullOrWhiteSpace(DocumentId));
            var minio = new MinioClient(OpenshiftConfig.MINIO_EndPoint, OpenshiftConfig.MINIO_AccessKey, OpenshiftConfig.MINIO_SecretKey);//.WithSSL();
            ObjectStat objectStat;
            objectStat = await minio.StatObjectAsync(TemplateId, DocumentId);
            Console.WriteLine("Object Stat: " + objectStat);

            var responseStream = new MemoryStream();
            await minio.GetObjectAsync(TemplateId, DocumentId, (stream) =>
            {
                stream.CopyTo(responseStream);
                responseStream.Position = 0;
                stream.Dispose();
            });

            return File(responseStream, objectStat.ContentType);
        }
    }
}