using System.Text.Json;

namespace NoPrint.Framework.Validation;

public class PropertyError : LogicalError
{
    public string PropTitle { get; set; }
    public override string ToJson() => JsonSerializer.Serialize(this);

}