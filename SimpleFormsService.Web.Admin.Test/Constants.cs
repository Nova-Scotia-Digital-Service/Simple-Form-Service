using System;
using System.Text.Json;
using SimpleFormsService.Domain;
using SimpleFormsService.Domain.Entities.Supporting.JSON;

namespace SimpleFormsService.Test;

public static class Constants
{
    public const string ConcreteDatabaseTestCollectionName = "Concrete Database xUnit Test Collection";
    public const string MockHttpContextUserName = "MockHttpContextUser";
    public const string MinioBucketName = "form-service";
    public const string DefaultUser = "SYSTEM";

    public static FormSubmissionData? GetFormSubmissionData(Guid submissionId, Guid templateId)
    {
        var jsonString =
            "{ " +
                "\"Identifier\": " +
                "{ \"GUID\": \"" + Guid.NewGuid() + "\", \"FriendlyName\": \"Friendly Name\" }," +
                "\"TemplateId\": \"" + templateId + "\"," +
                "\"DateSubmitted\": \"" + SystemTime.Now() + "\"," +
                "\"SubmissionStatus\": \"INITIALIZED\"," +
                "\"ConfirmationEmailAddresses\": [ " +
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
                "{ \"TemplateId\": \"" + templateId + "\", \"DocumentId\": \"documentId1\", \"Filename\": \"bioterrain.jpg\" }," +
                "{ \"TemplateId\": \"" + templateId + "\", \"DocumentId\": \"documentId2\", \"Filename\": \"holygrail.doc\" }" +
                "]," +
                "\"CreateUser\": \"" + MockHttpContextUserName + "\"," +
                "\"CreateDate\": \"" + SystemTime.Now() + "\"," +
                "\"UpdateUser\": \"" + MockHttpContextUserName + "\"," +
                "\"UpdateDate\": \"" + SystemTime.Now() + "\"" +
            "}";

        var formSubmissionData = JsonSerializer.Deserialize<FormSubmissionData>(jsonString);

        return formSubmissionData;
    }

    public static FormTemplateData? GetFormTemplateData()
    {
        var jsonString = 
            "{ " +
              "\"Identifier\": " +
              "{ \"GUID\": \"" + Guid.NewGuid() + "\", \"FriendlyName\": \"Friendly Name\" }," +
              "\"AdminNotifyEmailAddresses\": [ " +
              "{ \"EmailAddress\": \"sclaus@northpole.com\" }," +
              "{ \"EmailAddress\": \"ebunny@rabbithole.com\" }" +
              "]," +
              "\"AuthorizedUsers\": [" +
              "{ \"User\": \"authorizeduser1@email.com\" }," +
              "{ \"User\": \"authorizeduser2@email.com\" }," +
              "{ \"User\": \"MockHttpContextUser\" }" +
            "]" +
            "}";

        var formTemplateData = JsonSerializer.Deserialize<FormTemplateData>(jsonString);

        return formTemplateData;
    }
}