using NoPrint.Api.CustomPipe;
using NoPrint.Framework.Exceptions;
using System.Security.Authentication;

namespace NoPrint.Api;

public static class HttpExceptionHandler
{
    public static IResult ConvertToHttpResponse(this Exception exception)
    {
        if (exception is InvalidPropertyException invalidPropertyException)
            return Results.UnprocessableEntity(invalidPropertyException.Error);

        if (exception is AggregateInvalidPropertyException aggregateInvalidPropertyException)
            return Results.UnprocessableEntity(aggregateInvalidPropertyException.Error);
        
        if (exception is BadRequestException)
            return Results.BadRequest(exception.Message);
        
        if (exception is AuthenticationException)
            return Results.StatusCode(403);

        return Results.StatusCode(500);
    }
}