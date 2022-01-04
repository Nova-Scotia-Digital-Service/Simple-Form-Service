using System;
using SimpleFormsService.Domain.Exceptions.Abstract;

namespace SimpleFormsService.Domain.Exceptions
{
    public sealed class InvalidFormatException : BadRequestException
    {
        public InvalidFormatException(string argumentname) : base($"'{argumentname}' is not valid.")
        { }
    }
}
