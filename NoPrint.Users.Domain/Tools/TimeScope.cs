namespace NoPrint.Users.Domain.Tools;

public class TimeScope
{
    public TimeScope(int startMin, int scopeSize)
    {
        startMin = startMin % 60;
        StartMin = startMin + 39;

        DifferentHours = startMin - scopeSize < 0;

        if (DifferentHours) EndMin = 59 + (startMin - scopeSize) + 39;
        EndMin = startMin - scopeSize + 39;
    }

    public int StartMin { get; set; }
    public int EndMin { get; set; }

    public bool DifferentHours = false;

    public int GetCode()
    {
        return EndMin * EndMin - StartMin;
    }
}