namespace SimpleFormsService.Domain.Exceptions;
public static class Guard
{
    public static void AgainstNullEmptyOrWhiteSpace(string argumentValue, string argumentName)
    {
        if (string.IsNullOrWhiteSpace(argumentValue))
        {
            throw new NullEmptyOrWhitespaceException(argumentName);
        }
    }

    public static void AgainstInvalidGuidFormat(string argumentValue, string argumentName)
    {
        var tryParse = Guid.TryParse(argumentValue, out _);

        if (!tryParse)
        {
            throw new InvalidFormatException(argumentName);
        }
    }

    public static void AgainstObjectNotFound(object instance, string objectName)
    {
        if (instance is null)
        {
            throw new ObjectNotFoundException(objectName);
        }
    }

    public static void AgainstObjectNotFound(object instance, string objectName, string argumentValue, string argumentName)
    {
        if (instance is null)
        {
            throw new ObjectNotFoundException(objectName, argumentValue, argumentName);
        }
    }

    public static void AgainstEmptyList<T>(List<T> instance, string objectName, string argumentValue,
        string argumentName)
    {
        if (instance != null && instance.Count == 0)
        {
            throw new EmptyListException(objectName, argumentValue, argumentName);
        }
    }

    public static void AgainstNullOrEmptyList<T>(List<T> argumentValue, string argumentName)
    {
        if (argumentValue == null || argumentValue.FirstOrDefault() == null)
        {
            throw new NullOrEmptyException(argumentName);
        }
    }

    public static void AgainstNullOrEmptyObject(object instance, string argumentName)
    {
        if (instance is null)
        {
            throw new NullOrEmptyException(argumentName);
        }
    }
}