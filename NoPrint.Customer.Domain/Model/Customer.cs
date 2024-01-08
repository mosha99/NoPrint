using System.Runtime.CompilerServices;
using NoPrint.Framework.Exceptions;
using NoPrint.Framework.Validation;
using NoPrint.Users.Share.Interfaces;

namespace NoPrint.Customer.Domain.Model;

public class Customer : ILoginAbleEntityByCode , ILoginAbleEntityByPassword
{

    public static Customer CreateInstance(string customerName, string customerPhone, string customerAddress, string userName, string password)
    {
        customerName.ValidationCheck(nameof(CustomerName), x => x?.Length >= 3, "E1015");
        customerPhone.ValidationCheck(nameof(PhoneNumber), x => x?.Length == 11, "E1016");
        customerAddress.ValidationCheck(nameof(CustomerAddress), x => !string.IsNullOrWhiteSpace(x), "E1017");
        userName.ValidationCheck(nameof(UserName), x => x?.Length >= 5, "E1018");
        password.ValidationCheck(nameof(Password), x => x?.Length >= 5, "E1019");

        return new Customer()
        {
            CustomerName = customerName,
            PhoneNumber = customerPhone,
            CustomerAddress = customerAddress,
            UserName = userName,
            Password = password,
            FillProfile = true,
        };
    }
    public static Customer CreateInstance(string customerPhone)
    {
        customerPhone.ValidationCheck(nameof(PhoneNumber), x => x?.Length == 11, "E1016");


        return new Customer()
        {
            PhoneNumber = customerPhone,
            FillProfile = false,
        };
    }
    private Customer() { }

    public long UserId { get; private set; }
    public string CustomerName { get; private set; }
    public string CustomerAddress { get; private set; }
    public string UserName { get; private set; }
    public string Password { get; private set; }
    public string PhoneNumber { get; private init; }
    public bool FillProfile { get; private set; }
    public string Code { get; private set;}
    public bool CanLogin { get;private set; }
    public void Disable() => CanLogin = false;
    public void Enable() => CanLogin = true;
    public void FillData(string customerName, string customerAddress, string userName, string password)
    {
        if (FillProfile) throw new InvalidPropertyException(nameof(FillProfile), "E1023");

        customerName.ValidationCheck(nameof(CustomerName), x => x?.Length >= 3, "E1015");
        customerAddress.ValidationCheck(nameof(CustomerAddress), x => !string.IsNullOrWhiteSpace(x), "E1017");
        userName.ValidationCheck(nameof(UserName), x => x?.Length >= 5, "E1018");
        password.ValidationCheck(nameof(Password), x => x?.Length >= 5, "E1019");

        CustomerName = customerName;
        CustomerAddress = customerAddress;
        UserName = userName;
        Password = password;
        FillProfile = true;
    }
    public string GenerateCode() => Code = new Random().Next(1000,9999).ToString();
}