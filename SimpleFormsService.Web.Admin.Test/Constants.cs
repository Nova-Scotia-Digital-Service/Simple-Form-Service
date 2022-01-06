using System;
using System.Text.Json;
using SimpleFormsService.Domain.Entities.Supporting.JSON;

namespace SimpleFormsService.Test;

public static class Constants
{
    public const string ConcreteDatabaseTestCollectionName = "Concrete Database xUnit Test Collection";
    public const string MockHttpContextUserName = "MockHttpContextUser";
    public const string MinioBucketName = "form-service";

    public static FormSubmissionData? GetFormSubmissionData(Guid submissionId, Guid templateId)
    {
        var jsonString =
            "{ " +
                "\"SubmissionId\": \" " + submissionId + "\"," +
                "\"TemplateId\": \"" + templateId + "\"," +
                "\"DateSubmitted\": \"" + DateTime.Now + "\"," +
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
                "{ \"TemplateId\": \"" + templateId + "\", \"DocumentId\": \"documentId1\" }," +
                "{ \"TemplateId\": \"" + templateId + "\", \"DocumentId\": \"documentId2\" }" +
                "]," +
                "\"CreateUser\": \"" + MockHttpContextUserName + "\"," +
                "\"CreateDate\": \"" + DateTime.Now + "\"," +
                "\"UpdateUser\": \"" + MockHttpContextUserName + "\"," +
                "\"UpdateDate\": \"" + DateTime.Now + "\"" +
            "}";

        var formSubmissionData = JsonSerializer.Deserialize<FormSubmissionData>(jsonString);

        return formSubmissionData;
    }

    public static FormTemplateData? GetFormTemplateData()
    {
        const string jsonString = 
            "{ " +
              "\"Name\": \"Template Name\"," +
              "\"AuthorizedUsers\": [" +
              "{ \"EmailAddress\": \"authorizeduser1@email.com\" }," +
              "{ \"EmailAddress\": \"authorizeduser2@email.com\" }" +
              "]" +
            "}";

        var formTemplateData = JsonSerializer.Deserialize<FormTemplateData>(jsonString);

        return formTemplateData;
    }
}