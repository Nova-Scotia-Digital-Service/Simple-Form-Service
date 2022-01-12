using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SimpleFormsService.Domain.Exceptions;
using SimpleFormsService.Persistence;
using SimpleFormsService.Services.Abstractions;
using SimpleFormsService.Test.SharedFixtures;
using Xunit;

namespace SimpleFormsService.Test.TestFixtures.SimpleFormsService.Services.Application;

[Collection(Constants.ConcreteDatabaseTestCollectionName)]
public class FormTemplateSecurityServiceFixture : IClassFixture<ConcreteDatabaseSharedFixture<SimpleFormsServiceDbContext>>
{
    private readonly ConcreteDatabaseSharedFixture<SimpleFormsServiceDbContext> _sharedFixture;
    private readonly IServiceManager _serviceManager;

    public FormTemplateSecurityServiceFixture(ConcreteDatabaseSharedFixture<SimpleFormsServiceDbContext> sharedFixture)
    {
        _sharedFixture = sharedFixture;
        _serviceManager = sharedFixture.Container.GetService<IServiceManager>()!;
    }

    [Fact]
    public async void HasAccess_WhenCalledWithATemplateId_ANullEmptyOrWhitespaceExceptionShouldBeThrown()
    {
        var email = "nonexistent@email.com";

        Func<Task> action = () => Task.Run(() => _serviceManager.FormTemplateSecurityService.HasAccess(null).Result);
        var exception = await Record.ExceptionAsync(action);

        Assert.IsType<NullEmptyOrWhitespaceException>(exception.InnerException);
    }

    [Fact]
    public async void HasAccess_WhenCalledWithAnInvalidTemplateId_AnInvalidFormatExceptionShouldBeThrown()
    {
        var templateId = "1234";
        var email = "nonexistent@email.com";

        Func<Task> action = () => Task.Run(() => _serviceManager.FormTemplateSecurityService.HasAccess(templateId).Result);
        var exception = await Record.ExceptionAsync(action);

        Assert.IsType<InvalidFormatException>(exception.InnerException);
    }

    [Fact]
    public async void HasAccess_WhenCalledWithANullEmail_ANullEmptyOrWhitespaceExceptionShouldBeThrown()
    {
        var formTemplate = _sharedFixture.CreateFormTemplate();
        var templateId = formTemplate.Id.ToString();

        Func<Task> action = () => Task.Run(() => _serviceManager.FormTemplateSecurityService.HasAccess(templateId).Result);
        var exception = await Record.ExceptionAsync(action);

        Assert.IsType<NullEmptyOrWhitespaceException>(exception.InnerException);
    }
    
    [Fact]
    public void HasAccess_WhenCalledWithAnUnauthorizedEmail_FalseShouldBeReturned()
    {
        var formTemplate = _sharedFixture.CreateFormTemplate();
        var templateId = formTemplate.Id.ToString();
        var email = "unauthorized@email.com";

        var hasAccess = _serviceManager.FormTemplateSecurityService.HasAccess(templateId).Result;

        Assert.False(hasAccess);
    }

    [Fact]
    public void HasAccess_WhenCalledWithAnAuthorizedEmail_TrueShouldBeReturned()
    {
        var formTemplate = _sharedFixture.CreateFormTemplate();
        var templateId = formTemplate.Id.ToString();
        var email = "authorizeduser1@email.com";

        var hasAccess = _serviceManager.FormTemplateSecurityService.HasAccess(templateId).Result;

        Assert.True(hasAccess);
    }
}