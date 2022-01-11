using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleFormsService.Domain.Entities.Supporting.JSON;

[NotMapped]
public class Identifier
{
    public Identifier(string guid, string friendlyName)
    {
        GUID = guid;
        FriendlyName = friendlyName;
    }

    public string GUID { get; set; }
    public string FriendlyName { get; set; }
}