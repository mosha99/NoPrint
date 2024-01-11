using System.Reflection;
using System.Security.Cryptography;

namespace NoPrint.Framework.Identity;

public abstract class IdentityBase
{
    public long Id { get; init; }

    public static string GetSequenceBase<TId>()
    {
        var sequenceName = typeof(TId).Name +"_Seq";
        return sequenceName;
    }
}