using NoPrint.Framework.Validation;
using NoPrint.Users.Share.Interfaces;

namespace NoPrint.Shop.Domain.Models;

public class Shop : ILoginAbleEntityByPassword
{

    public static Shop CreateInstance(string shopName, string shopPhone, string shopAddress, string userName, string password)
    {
        shopName.ValidationCheck(nameof(ShopName) , x=> x?.Length >= 3, "E1015");
        shopPhone.ValidationCheck(nameof(PhoneNumber) , x=> x?.Length == 11, "E1016");
        shopAddress.ValidationCheck(nameof(ShopAddress) , x=> !string.IsNullOrWhiteSpace(x), "E1017");
        userName.ValidationCheck(nameof(UserName) , x => x?.Length >= 5, "E1018");
        password.ValidationCheck(nameof(Password) , x => x?.Length >= 5, "E1019");

        return new Shop()
        {
            ShopName = shopName,
            PhoneNumber = shopPhone,
            ShopAddress = shopAddress,
            UserName = userName,
            Password = password
        };
    }

    private Shop() { }

    public long UserId { get; private set; }
    public bool CanLogin { get; private set;}
    public void Disable() => CanLogin = false;
    public void Enable() => CanLogin = true;

    public string ShopName { get; private set; }
    public string ShopAddress { get; private set; }

    public string UserName { get; private init; }
    public string Password { get; private set; }
    public string PhoneNumber { get; private init; }
}
