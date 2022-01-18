using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SimpleFormsService.Domain.Entities.Supporting.JSON;

[NotMapped]
public class FormTemplateData 
{
    public FormTemplateData(Identifier identifier, NotifyEmailAddress[] adminNotifyEmailAddresses, AuthorizedUser[] authorizedUsers)
    {
        Identifier = identifier;
        AdminNotifyEmailAddresses = adminNotifyEmailAddresses;
        AuthorizedUsers = authorizedUsers;
    }

    [JsonPropertyOrder(1)]
    public Identifier Identifier { get; set; }
    [JsonPropertyOrder(2)]
    public NotifyEmailAddress[] AdminNotifyEmailAddresses { get; set; }
    [JsonPropertyOrder(3)]
    public AuthorizedUser[] AuthorizedUsers { get; set; }
}