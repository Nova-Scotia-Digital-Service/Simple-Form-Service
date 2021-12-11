using System;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleFormsService.Configuration;
using SimpleFormsService.Domain.Entities.FormSubmission;
using SimpleFormsService.Domain.Entities.FormTemplate;
using SimpleFormsService.Domain.Repositories;
using SimpleFormsService.Persistence;
using SimpleFormsService.Persistence.Repositories;
using SimpleFormsService.Services;
using SimpleFormsService.Services.Abstractions;
using Xunit;

namespace SimpleFormsService.Web.Admin.Test.SharedFixtures
{
    [CollectionDefinition(Constants.ConcreteDatabaseTestCollectionName)]
    public class ConcreteDatabaseSharedFixture<TContext> : ISharedFixture where TContext : DbContext
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IServiceManager _serviceManager;

        public ConcreteDatabaseSharedFixture()
        {
            Container = buildContainer();

            _repositoryManager = Container.GetService<IRepositoryManager>()!;
            _serviceManager = Container.GetService<IServiceManager>()!;
        }

        public IServiceProvider Container { get; set; }

        public Guid CreateFormTemplate()
        {
            var formTemplateId = Guid.NewGuid();
            var formTemplate = new FormTemplate(formTemplateId);

            _repositoryManager.FormTemplateRepository.Create(formTemplate);
            _ = _repositoryManager.UnitOfWork.SaveChangesAsync().Result;

            return formTemplateId;
        }

        public Guid CreateFormSubmission()
        {
            var formTemplateId = CreateFormTemplate();
            var formSubmissionId = Guid.NewGuid();
            var formSubmission = new FormSubmission(formSubmissionId, formTemplateId, Constants.GetFormSubmissionData());

            _repositoryManager.FormSubmissionRepository.Create(formSubmission);
            var saveChangesAsync = _repositoryManager.UnitOfWork.SaveChangesAsync();
            _ = saveChangesAsync.Result;

            return formSubmissionId;
        }

        #region Private Helpers

        private IServiceProvider buildContainer()
        {
            var services = new ServiceCollection();

            services.AddDbContext<SimpleFormsServiceDbContext>(options => options.UseNpgsql(AppConfig.GetConnectionString(),
                sqlOptions =>
                {
                    sqlOptions.MaxBatchSize(1);
                    sqlOptions.EnableRetryOnFailure(1);
                }));

            services.Scan(scan => scan.FromAssembliesOf(typeof(IRepositoryBase<>), typeof(RepositoryBase<>))
                .AddClasses(classes => classes.AssignableTo(typeof(IRepositoryBase<>)).Where(type => !type.IsGenericType), false)
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.Scan(scan => scan.FromAssembliesOf(typeof(IServiceBase), typeof(ServiceBase))
                .AddClasses(classes => classes.AssignableTo<IServiceBase>(), false)
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.AddScoped(typeof(IRepositoryManager), typeof(RepositoryManager));
            services.AddScoped(typeof(IServiceManager), typeof(ServiceManager));

            services.AddSingleton<IHttpContextAccessor, MockHttpContextAccessor>();

            return services.BuildServiceProvider();
        }
        
        #endregion
    }
}