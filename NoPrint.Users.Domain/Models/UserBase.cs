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
            TryLoginCount = 0,
            CanLogin = true,
            ExpireDate = expireDate,
            User = null,
            Visitor = null,
        };
    }
    public bool CanLogin { get; private set; }




    public int TryLoginCount { get; private set; }
    public UserExpireDate ExpireDate { get; private set; }
    public IReadOnlyList<LoginInstance> LoginInstances { get; private set; }
    public User? User { get; private set; }
    public Visitor? Visitor { get; private set; }


    public void SetUser(User user) => User = user ?? throw new InvalidPropertyException("Error_UserCanNotBeNull");
    public void SetVisitor(Visitor visitor) => Visitor = visitor ?? throw new InvalidPropertyException("Error_VisitorCanNotBeNull");
    private void BaseLoginCheck()
    {
        TryLoginCount.ValidationCheck(x => x <= 3, "Error_TryLoginLimit");
        ExpireDate.ValidationCheck(x => !x.IsExpire(), "Error_UserExpire");
        CanLogin.ValidationCheck(x => x, "Error_UserCanNotLogin");
    }
    public Guid LoginByUserName(string deviceInfo, int expireMin)
    {
        User.ValidationCheck(x => x is not null, "Error_CanNotLoginByPassword");

        BaseLoginCheck();

        var loginInstance = LoginInstance.CreateInstance(UserExpireDate.AfterMin(expireMin), deviceInfo);

        AddLoginInstances(loginInstance);

        return loginInstance.LoginId.Value;
    }
    public Guid LoginByPhone(ILoginAbleByPhone loginAbleByPhone, string code, string deviceInfo, int expireMin)
    {
        Visitor.ValidationCheck(x => x is not null, "Error_CanNotLoginByPhone");

        Visitor?.Validate(code, loginAbleByPhone);

        Visitor?.Clear();

        BaseLoginCheck();

        var loginInstance = LoginInstance.CreateInstance(UserExpireDate.AfterMin(expireMin), deviceInfo);

        AddLoginInstances(loginInstance);

        return loginInstance.LoginId.Value;
    }
    private void AddLoginInstances(LoginInstance loginInstance)
    {
        var loginInstances = LoginInstances?.ToList() ?? new List<LoginInstance>();

        loginInstances.RemoveAll(x => x?.ExpireDate?.IsExpire() == true);

        loginInstances.Add(loginInstance);

        LoginInstances = loginInstances;
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

