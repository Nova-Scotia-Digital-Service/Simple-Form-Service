using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleFormsService.Domain.Entities.Supporting.JSON;

[NotMapped]
public class AuthorizedUser
{
    public AuthorizedUser(string user)
    {
        User = user;
    }

    public string User { get; set; }
}