using NoPrint.Framework;
using NoPrint.Framework.Exceptions;
using NoPrint.Framework.Validation;
using NoPrint.Identity.Share;
using NoPrint.Users.Share;

namespace NoPrint.Customers.Domain.Model
{
    public class Customer : Aggregate<CustomerId> , ILoginAbleByPhone
    {

        public static Customer CreateInstance(string customerName, string customerPhone, string customerAddress, UserId userId)
        {
            customerName.ValidationCheck(nameof(CustomerName), x => x?.Length >= 3, "Error_Length_Min_3");
            customerPhone.ValidationCheck(nameof(PhoneNumber), x => x?.Length == 11, "Error_Length_Eql_11");
            customerAddress.ValidationCheck(nameof(CustomerAddress), x => !string.IsNullOrWhiteSpace(x), "Error_Required");
            userId.ValidationCheck(nameof(userId), x => x.Id != 0, "Error_Required");

            return new Customer()
            {
                CustomerName = customerName,
                PhoneNumber = customerPhone,
                CustomerAddress = customerAddress,
                User = userId,
                FillProfile = true,
            };
        }
        public static Customer CreateInstance(string customerPhone, UserId userId)
        {
            customerPhone.ValidationCheck(nameof(PhoneNumber), x => x?.Length == 11, "Error_Length_Eql_11");
            userId.ValidationCheck(nameof(userId), x => x.Id != 0, "Error_Required");

            return new Customer()
            {
                PhoneNumber = customerPhone,
                User = userId,
                FillProfile = false,
            };
        }


        private Customer() { }
        public UserId User { get; private set; }
        public string CustomerName { get; private set; }
        public string CustomerAddress { get; private set; }
        public string PhoneNumber { get; private set; }
        public bool FillProfile { get; private set; }
        public void FillData(string customerName, string customerAddress)
        {
            if (FillProfile) throw new InvalidPropertyException(nameof(FillProfile), "Error_CustomerFilledNow");

            CustomerName = customerName;
            CustomerAddress = customerAddress;
            FillProfile = true;
        }

        public string GetNameOrPhone() => CustomerName ?? PhoneNumber;
    }
}