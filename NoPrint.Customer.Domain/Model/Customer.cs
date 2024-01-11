using NoPrint.Framework;
using NoPrint.Framework.Exceptions;
using NoPrint.Framework.Validation;
using NoPrint.Identity.Share;

namespace NoPrint.Customers.Domain.Model
{
    public class Customer : Aggregate<CustomerId>
    {

        public static Customer CreateInstance(string customerName, string customerPhone, string customerAddress, UserId userId)
        {
            customerName.ValidationCheck(nameof(CustomerName), x => x?.Length >= 3, "E1015");
            customerPhone.ValidationCheck(nameof(PhoneNumber), x => x?.Length == 11, "E1016");
            customerAddress.ValidationCheck(nameof(CustomerAddress), x => !string.IsNullOrWhiteSpace(x), "E1017");
            userId.ValidationCheck(nameof(userId), x => x.Id != 0, "E1033");


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
            customerPhone.ValidationCheck(nameof(PhoneNumber), x => x?.Length == 11, "E1016");
            userId.ValidationCheck(nameof(userId), x => x.Id != 0, "E1033");

            return new Customer()
            {
                PhoneNumber = customerPhone,
                User = userId,
                FillProfile = false,
            };
        }


        private Customer() { }
        public UserId User { get; set; }
        public string CustomerName { get; private set; }
        public string CustomerAddress { get; private set; }
        public string PhoneNumber { get; private set; }
        public bool FillProfile { get; private set; }
        public void FillData(string customerName, string customerAddress)
        {
            if (FillProfile) throw new InvalidPropertyException(nameof(FillProfile), "E1023");

            customerName.ValidationCheck(nameof(CustomerName), x => x?.Length >= 3, "E1015");
            customerAddress.ValidationCheck(nameof(CustomerAddress), x => !string.IsNullOrWhiteSpace(x), "E1017");

            CustomerName = customerName;
            CustomerAddress = customerAddress;
            FillProfile = true;
        }
    }
}