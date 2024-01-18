using System.Text.Json;

namespace NoPrint.Framework.Validation;

public class PropertyError : LogicalError
{
    public PropertyError(string propTitle , List<string> errors) : base(errors)
    {
        PropTitle = propTitle;
    }

    public PropertyError()
    {
        
    }
    public string PropTitle { get; set; }
}