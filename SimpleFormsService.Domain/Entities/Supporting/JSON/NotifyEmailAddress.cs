using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleFormsService.Domain.Entities.Supporting.JSON;

[NotMapped]
public class NotifyEmailAddress
{
    public NotifyEmailAddress(string emailAddress)
    {
        EmailAddress = emailAddress;
    }

    public string EmailAddress { get; set; }
}