using System.Collections.Generic;

namespace SimpleFormsService.Domain.Exceptions
{

    public static class Guard
    {
        public static void AgainstNullEmptyOrWhiteSpace(string argumentValue, string argumentName)
        {
            if (string.IsNullOrWhiteSpace(argumentValue))
            {
                throw new NullEmptyOrWhitespaceException(argumentName);
            }
        }

        public static void AgainstObjectNotFound(object instance, string objectName, string argumentValue, string argumentName)
        {
            if (instance is null)
            {
                throw new ObjectNotFoundException(objectName, argumentValue, argumentName);
            }
        }

        public static void AgainstEmptyList<T>(List<T> instance, string objectName, string argumentValue, string argumentName)
        {
            if (instance != null && instance.Count == 0)
            {
                throw new EmptyListException(objectName, argumentValue, argumentName);
            }
        }

        public static void AgainstMaxStringLength(string code, string codeName, int maxLength)
        {
            if (code.Length > maxLength)
            {
                throw new StringLengthExcededException(codeName, maxLength);
            }
        }
    }
}
