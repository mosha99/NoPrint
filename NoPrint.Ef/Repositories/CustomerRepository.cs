using NoPrint.Application.Ef.Base;
using NoPrint.Customers.Domain.Model;
using NoPrint.Customers.Domain.Repository;
using NoPrint.Framework.Validation;
using NoPrint.Identity.Share;

namespace NoPrint.Application.Ef.Repositories;

public class CustomerRepository : RepositoryBase<Customer,CustomerId>, ICustomerRepository
{
    public CustomerRepository(NoPrintContext context) : base(context)
    {

    }

    public override Task<CustomerId> AddAsync(Customer entity)
    {
        _context.Set<Customer>().Any(x=>x.PhoneNumber.Equals(entity.PhoneNumber))
            .TrueCheck(nameof(Customer.PhoneNumber),"Error_PhoneIsUniq");

        return base.AddAsync(entity);
    }
}