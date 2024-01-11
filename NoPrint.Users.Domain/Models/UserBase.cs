using System.Diagnostics.CodeAnalysis;
using NoPrint.Framework;
using NoPrint.Framework.Exceptions;
using NoPrint.Framework.Validation;
using NoPrint.Identity.Share;
using NoPrint.Users.Domain.ValueObjects;

namespace NoPrint.Users.Domain.Models;

public class UserBase : Aggregate<UserId>
{
    private UserBase()
    {

    }

    public static UserBase CreateInstance(UserExpireDate expireDate)
    {
        return new UserBase()
        {
            LastLogin = null,
            TryLoginCount = 0,
            CanLogin = true,
            ExpireDate = expireDate,
            User = null,
            Visitor = null,
        };
    }


    public DateTime? LastLogin { get; private set; }
    public int TryLoginCount { get; private set; }
    public bool CanLogin { get; private set; }

    public UserExpireDate ExpireDate { get; private set; }
    public User? User { get; private set; }
    public Visitor? Visitor { get; private set; }


    public void SetUser(User user) => User = user ?? throw new InvalidPropertyException("E1026");
    public void SetVisitor(Visitor visitor) => Visitor = visitor ?? throw new InvalidPropertyException("E1027");

    private void BaseLoginCheck()
    {
        TryLoginCount.ValidationCheck(x => x <= 3, "E1032");
        ExpireDate.ValidationCheck(x => !x.IsExpire(), "E1032");
        CanLogin.ValidationCheck(x => x, "E1033");
    }

    public void LoginByUserName(string username, string password)
    {
        BaseLoginCheck();

        User.ValidationCheck(x => x is not null, "E1029");
        User.ValidationCheck(x => x?.Password?.Equals(password) == true, "E1030");
        User.ValidationCheck(x => x?.UserName?.Equals(username) == true, "E1031");

        LastLogin = DateTime.UtcNow;
    }

    public void LoginByPhone(string phoneNumber, string code)
    {
        BaseLoginCheck();

        Visitor.ValidationCheck(x => x is not null, "E1029");
        Visitor?.Validate(code);
        Visitor?.Clear();

        LastLogin = DateTime.UtcNow;
    }

    public void Disable() => CanLogin = true;
    public void Enable()
    {
        ExpireDate.ValidationCheck("ExpireDate", x => !x.IsExpire(), "E1025");
        CanLogin = true;
    }
}