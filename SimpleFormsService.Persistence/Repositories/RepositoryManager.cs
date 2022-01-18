using SimpleFormsService.Domain.Repositories;

namespace SimpleFormsService.Persistence.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<IFormSubmissionRepository> _lazyFormSubmissionRepository;
        private readonly Lazy<IFormTemplateRepository> _lazyFormTemplateRepository;
        private readonly Lazy<IUnitOfWork> _lazyUnitOfWork;

        public RepositoryManager(SimpleFormsServiceDbContext dbContext)
        {
            _lazyFormSubmissionRepository = new Lazy<IFormSubmissionRepository>(() => new FormSubmissionRepository(dbContext));
            _lazyFormTemplateRepository = new Lazy<IFormTemplateRepository>(() => new FormTemplateRepository(dbContext));
            _lazyUnitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(dbContext));
        }

        public IFormSubmissionRepository FormSubmissionRepository => _lazyFormSubmissionRepository.Value;
        public IFormTemplateRepository FormTemplateRepository => _lazyFormTemplateRepository.Value;
        public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;
    }
}