using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SimpleFormsService.Domain.Exceptions;
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
    
}