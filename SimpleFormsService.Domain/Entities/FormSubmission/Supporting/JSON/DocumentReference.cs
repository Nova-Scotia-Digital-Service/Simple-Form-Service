using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleFormsService.Domain.Entities.FormSubmission.Supporting.JSON;

[NotMapped]
public class DocumentReference
{
    public DocumentReference(string uri)
    {
        URI = uri;
    }

    public string URI { get; set; }
}