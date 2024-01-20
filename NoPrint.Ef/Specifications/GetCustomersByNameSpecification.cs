using Microsoft.EntityFrameworkCore;
using NoPrint.Application.Infra;
using NoPrint.Customers.Domain.Model;
using NoPrint.Framework.Specification;

namespace NoPrint.Application.Ef.Specifications;

public class GetCustomersByNameSpecification : IBaseGetListSpecification<Customer>
{
    private readonly string _name;

    public GetCustomersByNameSpecification(string name)
    {
        _name = name;
    }

    public async Task<ListResult<Customer>> GetAllAsync(IQueryable<Customer> queryable)
    {
        return new(await queryable.Where(x => x.CustomerName.Contains(_name)).ToListAsync()) ;
    }
}