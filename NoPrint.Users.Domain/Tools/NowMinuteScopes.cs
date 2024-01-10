namespace NoPrint.Users.Domain.Tools;

public class NowMinuteScopes
{
    public NowMinuteScopes()
    {
        minute = DateTime.Now.Minute;

        if (minute % 2 == 0)
        {
            EvenScope = minute + 39;
            OddScope = minute - 1 + 39;
        }
        else
        {
            EvenScope = minute + 39;
            OddScope = minute - 1 + 39;
        }

        if (OddScope > EvenScope) Code = OddScope * OddScope;
        else Code = EvenScope * EvenScope;
    }

    private int Code;

    public int minute;

    public int GetBestCode() => ToCrazy(Code + GetUniqEr());
    public int GetCode1() => ToCrazy(OddScope * OddScope + GetUniqEr());
    public int GetCode2() => ToCrazy(EvenScope * EvenScope + GetUniqEr());

    public bool CodeIsValid(int code)
    {
        return code == ToCrazy(OddScope * OddScope + GetUniqEr()) || code == ToCrazy(EvenScope * EvenScope + GetUniqEr());
    }

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

    public int EvenScope { get; private set; }
    public int OddScope { get; private set; }
}