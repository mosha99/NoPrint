using NoPrint.Framework.Validation;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NoPrint.Framework.Exceptions;

public class InvalidPropertyException : Exception
{
    public InvalidPropertyException(string propertyName,params string[] errors)
    {
        Error = new PropertyError()
        {
            Errors = errors.ToList(),
            PropTitle = propertyName

        };
    }
    public InvalidPropertyException(params string[] errors)
    {
        Error = new LogicalError()
        {
            Errors = errors.ToList(),
        };
    }

    public LogicalError Error { get; private set; }
}
