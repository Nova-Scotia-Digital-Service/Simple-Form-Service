using SimpleFormsService.Domain.Exceptions.Abstract;

namespace SimpleFormsService.Domain.Exceptions
{
    public sealed class StringLengthExcededException : BadRequestException
    {
        public StringLengthExcededException(string argumentName, int maxLengthValue) : base($"The '{argumentName}' field has exceeded the maximum string length of {maxLengthValue}.")
        { }
    }
}
