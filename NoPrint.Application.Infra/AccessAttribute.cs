namespace NoPrint.Application.Infra;


public class AccessAttribute : Attribute
{
    public Rule[] Accessors { get; }

    public AccessAttribute(params Rule[] accessors)
    {
        Accessors = accessors;
    }
}