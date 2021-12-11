using System;
using SimpleFormsService.Domain.Exceptions.Abstract;

namespace SimpleFormsService.Domain.Exceptions
{
    public sealed class EmptyListException : NotFoundException
    {
        public EmptyListException(string objectName, string argumentValue, string argumentName) : base($"No {objectName} with the {argumentName} '{argumentValue}' were found.")
        { }
    }
}
