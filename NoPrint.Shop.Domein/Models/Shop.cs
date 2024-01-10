using NoPrint.Framework;
using NoPrint.Framework.Validation;
using Noprint.Identity.Share;

namespace NoPrint.Shops.Domain.Models;

public class Shop : Aggregate<ShopId>
{

    public static Shop CreateInstance(string shopName, string shopPhone, string shopAddress, string userName, string password , UserId userId)
    {
        shopName.ValidationCheck(nameof(ShopName) , x=> x?.Length >= 3, "E1015");
        shopPhone.ValidationCheck(nameof(PhoneNumber) , x=> x?.Length == 11, "E1016");
        shopAddress.ValidationCheck(nameof(ShopAddress) , x=> !string.IsNullOrWhiteSpace(x), "E1017");
        userId.ValidationCheck(nameof(userId) , x=> x.Id != 0, "E1017");


        return new Shop()
        {
            ShopName = shopName,
            PhoneNumber = shopPhone,
            ShopAddress = shopAddress,
            User = userId,
        };
    }

    private Shop() { }
    public UserId User { get; private set; } 
    public string ShopName { get; private set; }
    public string ShopAddress { get; private set; }
    public string PhoneNumber { get; private init; }
}
