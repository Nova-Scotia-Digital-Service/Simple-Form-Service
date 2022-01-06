using System;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Minio.AspNetCore;
using SimpleFormsService.Configuration;
using SimpleFormsService.Domain.Entities;
using SimpleFormsService.Domain.Repositories;
using SimpleFormsService.Persistence;
using SimpleFormsService.Persistence.Repositories;
using SimpleFormsService.Services;
using SimpleFormsService.Services.Abstractions;
using Xunit;

namespace SimpleFormsService.Test.SharedFixtures
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

        public FormTemplate CreateFormTemplate()
        {
            var formTemplate = new FormTemplate(Guid.NewGuid(), Constants.GetFormTemplateData());

            _repositoryManager.FormTemplateRepository.Create(formTemplate);
            _ = _repositoryManager.UnitOfWork.SaveChangesAsync().Result;

            return formTemplate;
        }

        public FormSubmission CreateFormSubmission(FormTemplate formTemplate)
        {
            var formSubmissionId = Guid.NewGuid();
            var formSubmission = new FormSubmission(formSubmissionId, formTemplate.Id, Constants.GetFormSubmissionData(formSubmissionId, formTemplate.Id));

            _repositoryManager.FormSubmissionRepository.Create(formSubmission);
            _ = _repositoryManager.UnitOfWork.SaveChangesAsync().Result;

            return formSubmission;
        }

        #region Private Helpers

        private IServiceProvider buildContainer()
        {
            var services = new ServiceCollection();

            services.AddDbContext<SimpleFormsServiceDbContext>(options => options.UseNpgsql(AppConfig.Postgres_ConnectionString,
                sqlOptions =>
                {
                    sqlOptions.MaxBatchSize(1);
                    sqlOptions.EnableRetryOnFailure(1);
                })
                .EnableDetailedErrors());

            services.AddMinio(options =>
            {
                options.Endpoint = OpenshiftConfig.MINIO_EndPoint;
                options.AccessKey = OpenshiftConfig.MINIO_AccessKey;
                options.SecretKey = OpenshiftConfig.MINIO_SecretKey;
            });

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