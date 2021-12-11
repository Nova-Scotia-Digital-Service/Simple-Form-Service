namespace SimpleFormsService.Domain.Repositories
{
    public interface IRepositoryManager
    {
        IFormSubmissionRepository FormSubmissionRepository { get; }
        IFormTemplateRepository FormTemplateRepository { get; }
        IUnitOfWork UnitOfWork { get; }
    }
}
