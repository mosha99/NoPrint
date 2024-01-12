using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using NoPrint.Framework.Specification;
using NoPrint.Framework.Validation;
using NoPrint.Users.Domain.Models;
using System.Numerics;
using NoPrint.Customers.Domain.Model;

namespace NoPrint.Ef.Specifications;

public class GetCustomerByPhoneSpecification : IBaseGetSpecification<Customer>
{
    private readonly string _phone;

    public GetCustomerByPhoneSpecification(string phone)
    {
        phone.ValidationCheck("PhoneNumber", x => !string.IsNullOrWhiteSpace(x), "E1034");
        _phone = phone;
    }
    public async Task<Customer?> GetAsync(IQueryable<Customer> queryable)
    {
        return await queryable.SingleOrDefaultAsync(x => x.PhoneNumber == _phone);
    }
}

