using System.Text.Json;

namespace NoPrint.Framework.Validation;

public class LogicalError
{
    public List<string> Errors { get; set; }
    public virtual string ToJson() => JsonSerializer.Serialize(this);
}