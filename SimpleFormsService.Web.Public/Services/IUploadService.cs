
namespace SimpleFormsService.Web.Public.Services
{
    public interface IUploadService
    {
        string UploadToBlob(string fileName, byte[] content, string fileMimeType);
        void DeleteFromBlob();
    }
}
