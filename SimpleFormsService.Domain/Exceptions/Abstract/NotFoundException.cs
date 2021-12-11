using System;

namespace SimpleFormsService.Domain.Exceptions.Abstract
{
    public abstract class NotFoundException : Exception
    {
        protected NotFoundException(string message) : base(message)
        { }
    }
}
