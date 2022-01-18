using System;

namespace SimpleFormsService.Domain.Exceptions.Abstract
{
    public abstract class BadRequestException : Exception
    {
        protected BadRequestException(string message) : base(message)
        { }
    }
}