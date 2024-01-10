namespace NoPrint.Users.Domain.Tools;

public class TimeScopeCodeGenerator
{
    public int ScopeSize;

    public TimeScopeCodeGenerator(int scopeSize)
    {
        ScopeSize = scopeSize + 1;
        TimeScopes = new List<TimeScope>();
        FillTimeScopes();
    }

    public List<TimeScope> TimeScopes { get; private set; }

    private void FillTimeScopes()
    {
        int nowMin = DateTime.Now.Minute;

        for (int i = nowMin + 1; i < nowMin + ScopeSize; i++)
        {
            TimeScopes.Add(new TimeScope(i, ScopeSize));
        }

        //Console.WriteLine(JsonSerializer.Serialize(TimeScopes));
    }

    public int GetCode() => GetCode(TimeScopes.Count - 1);
    private int GetCode(int index) => ToCrazy(TimeScopes[index].GetCode() + GetUniqEr());

    private int GetUniqEr()
    {
        var t = TimeOnly.FromDateTime(DateTime.Now).Hour.ToString().GetHashCode().ToString().Reverse().ToList();
        var d = DateOnly.FromDateTime(DateTime.Now).ToString().GetHashCode().ToString().Reverse().ToList();

        return int.Parse($"{d[1]}{t[3]}{d[5]}{t[7]}");
    }
    private int ToCrazy(int code)
    {
        var gc = code.ToString().Reverse().Take(4).ToList();
        if (gc[2] == '0') gc[2] = '3';
        return int.Parse($"{gc[2]}{gc[0]}{gc[3]}{gc[1]}");
    }


    public bool CodeIsValid<T>(T generatedCode, Func<int, T> codeGenerator)
    {
        for (int i = 0; i < TimeScopes.Count; i++)
        {
            var c = codeGenerator(GetCode(i));
            if (c.Equals(generatedCode)) return true;
        }
        return false;
    }
}