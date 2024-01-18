using Microsoft.EntityFrameworkCore;
using NoPrint.Customers.Domain.Model;
using NoPrint.Framework.Specification;
using NoPrint.Framework.Validation;

namespace NoPrint.Application.Ef.Specifications;

public class GetCustomerByPhoneSpecification : IBaseGetSpecification<Customer>
{
    private readonly string _phone;

    public GetCustomerByPhoneSpecification(string phone)
    {
        phone.ValidationCheck("CustomerPhoneNumber", x => !string.IsNullOrWhiteSpace(x), "Error_Required");
        _phone = phone;
    }
    public async Task<Customer?> GetAsync(IQueryable<Customer> queryable)
    {
        return await queryable.SingleOrDefaultAsync(x => x.PhoneNumber == _phone);
    }
}

