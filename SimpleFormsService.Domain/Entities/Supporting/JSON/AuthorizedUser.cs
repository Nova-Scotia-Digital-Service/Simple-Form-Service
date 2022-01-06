using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleFormsService.Domain.Entities.Supporting.JSON;

[NotMapped]
public class AuthorizedUser
{
    public AuthorizedUser(string emailAddress)
    {
        EmailAddress = emailAddress;
    }

    public string EmailAddress { get; set; }
}