using Microsoft.Extensions.DependencyInjection;
using SimpleFormsService.Persistence;
using SimpleFormsService.Services.Abstractions;
using SimpleFormsService.Test.SharedFixtures;
using Xunit;

namespace SimpleFormsService.Test.TestFixtures.SimpleFormsService.Services.Domain;

[Collection(Constants.ConcreteDatabaseTestCollectionName)]
public class FormTemplateSecurityFixture : IClassFixture<ConcreteDatabaseSharedFixture<SimpleFormsServiceDbContext>>
{
    private readonly ConcreteDatabaseSharedFixture<SimpleFormsServiceDbContext> _sharedFixture;
    private readonly IServiceManager _serviceManager;

    public FormTemplateSecurityFixture(ConcreteDatabaseSharedFixture<SimpleFormsServiceDbContext> sharedFixture)
    {
        _sharedFixture = sharedFixture;
        _serviceManager = sharedFixture.Container.GetService<IServiceManager>()!;
    }

    [Fact]
    public async void GetFormTemplateByIdAsync_WhenCalledWithAValidTemplateIdAndAuthorizedUser_AFormTemplateShouldBeReturned()
    {
        var formTemplate = _sharedFixture.CreateFormTemplate();
        var templateId = formTemplate.Id.ToString();

        formTemplate = await _serviceManager.FormTemplateService.GetFormTemplateByIdAsync(templateId);

        Assert.True(formTemplate != null);
    }

    [Fact]
    public async void GetFormTemplatesAsync_WhenCalled_AFormTemplateIdentifierListShouldBeReturned()
    {
        _sharedFixture.CreateFormTemplate();

        var formTemplateIdentifiers = await _serviceManager.FormTemplateService.GetFormTemplatesAsync();

        Assert.True(formTemplateIdentifiers != null);
    }
}