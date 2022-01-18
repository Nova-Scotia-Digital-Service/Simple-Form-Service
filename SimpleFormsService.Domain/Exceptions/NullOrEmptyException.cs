using System;
using SimpleFormsService.Domain.Exceptions.Abstract;

namespace SimpleFormsService.Domain.Exceptions
{
    public sealed class NullOrEmptyException : BadRequestException
    {
        public NullOrEmptyException(string argumentname) : base($"'{argumentname}' cannot be null or empty.")
        { }
    }
}
