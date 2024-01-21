using NoPrint.Framework.Validation;

namespace NoPrint.Users.Domain.ValueObjects;

public class LoginInstance
{
    private LoginInstance()
    {
    }

    public static LoginInstance CreateInstance(UserExpireDate expireDate, string deviceInfo)
    {
        return new LoginInstance()
        {
            DeviceInfo = deviceInfo,
            ExpireDate = expireDate,
            LastLogin = DateTime.Now,
            LoginId = Guid.NewGuid(),
        };
    }

    public UserExpireDate ExpireDate { get; private set; }
    public DateTime? LastLogin { get; private set; }
    public Guid? LoginId { get; private set; }
    public string DeviceInfo { get; private set; }


    public Guid Next(bool simpleMode = false)
    {
        ExpireDate.ValidationCheck(x=>!x.IsExpire(),"Error_Login_Expire");

        if(!simpleMode)LoginId = Guid.NewGuid();

        LastLogin = DateTime.Now;

        return LoginId.Value;
    }
}