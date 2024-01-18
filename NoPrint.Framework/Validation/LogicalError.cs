using System.Text.Json;

namespace NoPrint.Framework.Validation;

public class LogicalError
{
    public LogicalError(List<string> errors)
    {
        Errors = errors;
    }

    public LogicalError()
    {
        
    }
    public List<string> Errors { get; set; }
}