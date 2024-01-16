using NoPrint.Framework.Validation;

namespace NoPrint.Users.Domain.ValueObjects;

public class UserExpireDate
{
    public UserExpireDate(DateTime? expireDate)
    {
        expireDate.ValidationCheck("ExpireDate", x => x is null | x > DateTime.Now, "Error_NotValid");
        ExpireDate = expireDate;
    }

    public UserExpireDate()
    {
        ExpireDate = null;
    }

    public DateTime? ExpireDate { get; private set; }
    public static UserExpireDate Empty => new UserExpireDate(null);

    public bool IsExpire() => ExpireDate != null && ExpireDate <= DateTime.Now;
}