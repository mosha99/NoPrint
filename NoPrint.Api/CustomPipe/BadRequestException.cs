using NoPrint.Framework.Exceptions;

namespace NoPrint.Api.CustomPipe;

public class BadRequestException : Exception 
{
    public BadRequestException()
    {
    }

    public BadRequestException(string message) : base(message)
    {
    }

    public BadRequestException(string message, Exception inner) : base(message, inner)
    {
    }
}