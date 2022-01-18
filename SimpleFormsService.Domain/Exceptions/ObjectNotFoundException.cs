using System;
using SimpleFormsService.Domain.Exceptions.Abstract;

namespace SimpleFormsService.Domain.Exceptions
{
    public sealed class ObjectNotFoundException : NotFoundException
    {
        public ObjectNotFoundException(string objectName, string argumentValue, string argumentName) : base($"The {objectName} with the {argumentName} '{argumentValue}' was not found.")
        { }
    }
}
