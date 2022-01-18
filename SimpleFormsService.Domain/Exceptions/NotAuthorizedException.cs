using SimpleFormsService.Domain.Exceptions.Abstract;

namespace SimpleFormsService.Domain.Exceptions
{
    public sealed class NotAuthorizedException : BadRequestException
    {
        public NotAuthorizedException(string objectName, string objectId) : base($"The current user is not authorized to view the {objectName} with id '{objectId}'")
        { }
    }
}
