using NoPrint.Application.Infra;

namespace NoPrint.Ui.Blazor.Tools;

[AttributeUsage(AttributeTargets.All)]
public class PageAccessAttribute(Rule _rule) : Attribute
{
    public bool HasAccess(Rule rule) => _rule.HasFlag(rule);
}