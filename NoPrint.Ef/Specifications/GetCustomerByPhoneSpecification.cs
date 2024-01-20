using Microsoft.EntityFrameworkCore;
using NoPrint.Application.Infra;
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

public class GetCustomerByPartOfPhoneSpecification : IBaseGetListSpecification<Customer>
{
    private readonly string _phone;

    public GetCustomerByPartOfPhoneSpecification(string phone)
    {
        _phone = phone;
    }
    public async Task<ListResult<Customer>> GetAllAsync(IQueryable<Customer> queryable)
    {
        return new(await queryable.Where(x => x.PhoneNumber.Contains(_phone)).ToListAsync());
    }
}