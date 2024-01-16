﻿using NoPrint.Framework;
using NoPrint.Framework.Validation;
using NoPrint.Identity.Share;

namespace NoPrint.Shops.Domain.Models;

public class Shop : Aggregate<ShopId>
{

    public static Shop CreateInstance(string shopName, string shopPhone, string shopAddress , UserId userId)
    {
        shopName.ValidationCheck(nameof(ShopName) , x=> x?.Length >= 3, "Error_Length_Min_3");
        shopPhone.ValidationCheck(nameof(PhoneNumber) , x=> x?.Length == 11, "Error_Length_Eql_11");
        shopAddress.ValidationCheck(nameof(ShopAddress) , x=> !string.IsNullOrWhiteSpace(x), "Error_Required");
        userId.ValidationCheck(nameof(userId) , x=> x.Id != 0, "Error_Required");


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
