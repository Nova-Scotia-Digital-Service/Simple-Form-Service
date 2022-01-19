namespace SimpleFormsService.Domain.Entities.Supporting;

public class FileStreamResultAdapter
{
    public FileStreamResultAdapter(string contentType, MemoryStream memoryStream)
    {
        ContentType = contentType;
        MemoryStream = memoryStream;
    }

    public string ContentType { get; set; }
    public MemoryStream MemoryStream { get; set; }
}