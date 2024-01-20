using System.Diagnostics.CodeAnalysis;
using NoPrint.Framework;
using NoPrint.Framework.Exceptions;
using NoPrint.Framework.Validation;
using NoPrint.Identity.Share;
using NoPrint.Notification.Share;
using NoPrint.Users.Domain.ValueObjects;
using NoPrint.Users.Share;

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
    public Guid LoginId { get; private set; }
    

    public void SetUser(User user) => User = user ?? throw new InvalidPropertyException("Error_UserCanNotBeNull");
    public void SetVisitor(Visitor visitor) => Visitor = visitor ?? throw new InvalidPropertyException("Error_VisitorCanNotBeNull");
    private void BaseLoginCheck()
    {
        TryLoginCount.ValidationCheck(x => x <= 3, "Error_TryLoginLimit");
        ExpireDate.ValidationCheck(x => !x.IsExpire(), "Error_UserExpire");
        CanLogin.ValidationCheck(x => x, "Error_UserCanNotLogin");
        LoginId = Guid.NewGuid();
    }
    public Guid LoginByUserName()
    {
        User.ValidationCheck(x => x is not null, "Error_CanNotLoginByPassword");

        BaseLoginCheck();

        LastLogin = DateTime.UtcNow;

        return LoginId;
    }
    public Guid LoginByPhone(ILoginAbleByPhone loginAbleByPhone, string code)
    {
        Visitor.ValidationCheck(x => x is not null, "Error_CanNotLoginByPhone");

        Visitor?.Validate(code, loginAbleByPhone);

        Visitor?.Clear();

        BaseLoginCheck();

        LastLogin = DateTime.UtcNow;

        return LoginId;
    }
    public void SendCode(ILoginAbleByPhone loginAbleByPhone, IMessageSenderService senderService)
    {

        Visitor.ValidationCheck(x => x != null, "Error_SendNotSupport");

        var code = Visitor.GenerateCode(loginAbleByPhone);

        senderService.Send(loginAbleByPhone.PhoneNumber, $"Code is : {code}");
    }
    public void Disable() => CanLogin = true;
    public void Enable()
    {
        ExpireDate.ValidationCheck("ExpireDate", x => !x.IsExpire(), "Error_UserExpire");
        CanLogin = true;
    }
}

