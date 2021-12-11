using System;
using SimpleFormsService.Domain.Exceptions.Abstract;

namespace SimpleFormsService.Domain.Exceptions
{
    public sealed class NullEmptyOrWhitespaceException : BadRequestException
    {
        public NullEmptyOrWhitespaceException(string argumentname) : base($"'{argumentname}' cannot be null, empty or whitespace.")
        { }
    }
}
