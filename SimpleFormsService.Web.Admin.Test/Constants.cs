using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
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
                "\"SubmissionId\": \" " + submissionId + "\"," +
                "\"TemplateId\": \"" + templateId + "\"," +
                "\"DateSubmitted\": \"" + SystemTime.Now() + "\"," +
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
        const string jsonString = 
            "{ " +
              "\"Name\": \"Template Name\"," +
              "\"NotifyEmailAddresses\": [ " +
              "{ \"EmailAddress\": \"sclaus@northpole.com\" }," +
              "{ \"EmailAddress\": \"ebunny@rabbithole.com\" }" +
              "]," +
              "\"AuthorizedUsers\": [" +
              "{ \"EmailAddress\": \"authorizeduser1@email.com\" }," +
              "{ \"EmailAddress\": \"authorizeduser2@email.com\" }" +
              "]" +
            "}";

        var formTemplateData = JsonSerializer.Deserialize<FormTemplateData>(jsonString);

        return formTemplateData;
    }
}