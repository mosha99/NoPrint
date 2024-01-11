namespace NoPrint.Framework;

public class SequenceNameAttribute : Attribute
{
    public readonly string SequenceName;

    public SequenceNameAttribute(string sequenceName)
    {
        SequenceName = sequenceName;
    }
}