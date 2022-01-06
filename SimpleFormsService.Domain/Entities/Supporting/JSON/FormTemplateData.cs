using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SimpleFormsService.Domain.Entities.Supporting.JSON;

[NotMapped]
public class FormTemplateData 
{
    public FormTemplateData(string name, AuthorizedUser[] authorizedUsers) 
    {
        Name = name;
        AuthorizedUsers = authorizedUsers;
    }

    [JsonPropertyOrder(1)]
    public string Name { get; set; }
    [JsonPropertyOrder(2)]
    public AuthorizedUser[] AuthorizedUsers { get; set; }
}