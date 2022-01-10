using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SimpleFormsService.Domain.Entities.Supporting.JSON;

[NotMapped]
public class FormTemplateData 
{
    public FormTemplateData(string name, NotifyEmailAddress[] notifyEmailAddresses, AuthorizedUser[] authorizedUsers) 
    {
        Name = name;
        NotifyEmailAddresses = notifyEmailAddresses;
        AuthorizedUsers = authorizedUsers;
    }

    [JsonPropertyOrder(1)]
    public string Name { get; set; }
    [JsonPropertyOrder(2)]
    public NotifyEmailAddress[] NotifyEmailAddresses { get; set; }
    [JsonPropertyOrder(3)]
    public AuthorizedUser[] AuthorizedUsers { get; set; }
    
}