namespace SimpleFormsService.Services.Abstractions
{
    public interface IServiceManager
    {
        IFormTemplateService FormTemplateService { get; }
        IFormSubmissionService FormSubmissionService { get; }
    }
}