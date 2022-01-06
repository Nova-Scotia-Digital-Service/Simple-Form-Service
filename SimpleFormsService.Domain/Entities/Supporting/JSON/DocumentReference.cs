using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleFormsService.Domain.Entities.Supporting.JSON;

[NotMapped]
public class DocumentReference
{
    public DocumentReference(string templateId, string documentId)
    {
        TemplateId = templateId;
        DocumentId = documentId;
    }

    public string TemplateId { get; set; } // minio bucket name
    public string DocumentId { get; set; } // minio object name
}