using NoPrint.Framework.Validation;

namespace NoPrint.Framework.Exceptions;

public class AggregateInvalidPropertyException : Exception
{
    public AggregateInvalidPropertyException(List<LogicalError> errors)
    {
        Error = errors.Cast<object>().ToList();
    }

    public List<object> Error { get; private set; }

}