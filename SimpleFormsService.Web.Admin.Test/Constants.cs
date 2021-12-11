using System.Text.Json;
using SimpleFormsService.Domain.Entities.FormSubmission.Supporting.JSON;

namespace SimpleFormsService.Web.Admin.Test;

public static class Constants
{
    public const string ConcreteDatabaseTestCollectionName = "Concrete Database xUnit Test Collection";

    public static FormSubmissionData? GetFormSubmissionData()
    {
        var jsonString =
            "{ " +
            "\"DateSubmitted\": \"2021-12-09 17:07:25\"," +
            "\"SubmissionStatus\": \"INITIALIZED\"," +
            "\"NotifyEmailAddresses\": [ " +
            "{ \"EmailAddress\": \"sclaus@northpole.com\" }," +
            "{ \"EmailAddress\": \"ebunny@rabbithole.com\" }" +
            "]," +
            "\"FormItems\": [" +
            " { \"Name\": \"Email\", \"Value\": \"johndoe@gmail.com\" }," +
            " { \"Name\": \"Name\", \"Value\": \"John Doe\" }," +
            " { \"Name\": \"Phone\", \"Value\": \"902-000-000\" }," +
            " { \"Name\": \"Submission Type\", \"Value\": \"New Application\" }" +
            "]," +
            "\"DocumentReferences\": [" +
            "{ \"URI\": \"/pathtodocumentminusthehost/1\" }," +
            "{ \"URI\": \"/pathtodocumentminusthehost/2\" }" +
            "]" +
            "}";

        var formSubmissionData = JsonSerializer.Deserialize<FormSubmissionData>(jsonString);

        return formSubmissionData;
    }
}