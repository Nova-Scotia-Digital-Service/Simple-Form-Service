using Minio;
using SimpleFormsService.Domain.Repositories;
using SimpleFormsService.Services.Abstractions;
using SimpleFormsService.Services.Abstractions.Application;
using SimpleFormsService.Services.Abstractions.Domain;
using SimpleFormsService.Services.Application;
using SimpleFormsService.Services.Domain;

namespace SimpleFormsService.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IFormSubmissionService> _lazyFormSubmissionService;
        private readonly Lazy<IFormTemplateService> _lazyFormTemplateService;
        private readonly Lazy<IDocumentService> _lazyMinIoDocumentService;

        public ServiceManager(IRepositoryManager repositoryManager, MinioClient client)
        {
            _lazyFormSubmissionService = new Lazy<IFormSubmissionService>(() => new FormSubmissionService(repositoryManager));
            _lazyFormTemplateService = new Lazy<IFormTemplateService>(() => new FormTemplateService(repositoryManager));
            _lazyMinIoDocumentService = new Lazy<IDocumentService>(() => new MinIoDocumentService(client)); 
        }

        public IFormSubmissionService FormSubmissionService => _lazyFormSubmissionService.Value;
        public IFormTemplateService FormTemplateService => _lazyFormTemplateService.Value;
        public IDocumentService MinIoDocumentService => _lazyMinIoDocumentService.Value;
    }
}