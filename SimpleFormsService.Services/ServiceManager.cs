using SimpleFormsService.Domain.Repositories;
using SimpleFormsService.Services.Abstractions;

namespace SimpleFormsService.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IFormSubmissionService> _lazyFormSubmissionService;
        private readonly Lazy<IFormTemplateService> _lazyFormTemplateService;

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _lazyFormSubmissionService = new Lazy<IFormSubmissionService>(() => new FormSubmissionService(repositoryManager));
            _lazyFormTemplateService = new Lazy<IFormTemplateService>(() => new FormTemplateService(repositoryManager));
        }

        public IFormSubmissionService FormSubmissionService => _lazyFormSubmissionService.Value;
        public IFormTemplateService FormTemplateService => _lazyFormTemplateService.Value;
    }
}