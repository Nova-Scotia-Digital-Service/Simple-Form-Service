using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using SimpleFormsService.Persistence;
using SimpleFormsService.Services.Abstractions.Application;
using SimpleFormsService.Test.SharedFixtures;
using Xunit;

namespace SimpleFormsService.Test.TestFixtures.SimpleFormsService.Services.Application
{
    [Collection(Constants.ConcreteDatabaseTestCollectionName)]
    public class GCNotificationServiceFixture : IClassFixture<ConcreteDatabaseSharedFixture<SimpleFormsServiceDbContext>>
    {
        private readonly ConcreteDatabaseSharedFixture<SimpleFormsServiceDbContext> _sharedFixture;
        private readonly INotificationService _notificationService;

        public GCNotificationServiceFixture(ConcreteDatabaseSharedFixture<SimpleFormsServiceDbContext> sharedFixture)
        {
            _sharedFixture = sharedFixture;
            _notificationService = sharedFixture.Container.GetService<INotificationService>()!;
        }

        [Fact]
        public void SendNotification_WhenCalled_ANotificationEmailShouldBeSent()
        {
            var formTemplate = _sharedFixture.CreateFormTemplate();
            var formSubmission = _sharedFixture.CreateFormSubmission(formTemplate);
            var emailAddresses = new List<string> { "craig.robinson@novascotia.ca" };

            _notificationService.SendConfirmationNotification(formTemplate, formSubmission, emailAddresses);

        }
    }
}