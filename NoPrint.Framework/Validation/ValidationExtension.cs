using NoPrint.Framework.Exceptions;

namespace NoPrint.Framework.Validation;

public static class ValidationExtension
{
    public static void ValidationCheck<T>(this T value ,string name, Func<T,bool> validation , params string[] errors)
    {
        if (!validation(value)) throw new InvalidPropertyException(name, errors);
    }
}