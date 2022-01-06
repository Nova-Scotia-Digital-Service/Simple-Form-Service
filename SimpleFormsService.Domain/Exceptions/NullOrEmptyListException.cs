using System;
using SimpleFormsService.Domain.Exceptions.Abstract;

namespace SimpleFormsService.Domain.Exceptions
{
    public sealed class NullOrEmptyListException : BadRequestException
    {
        public NullOrEmptyListException(string argumentname) : base($"'{argumentname}' cannot be null or empty.")
        { }
    }
}
