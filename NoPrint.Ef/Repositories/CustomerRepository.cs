using NoPrint.Application.Ef.Base;
using NoPrint.Customers.Domain.Model;
using NoPrint.Customers.Domain.Repository;
using NoPrint.Identity.Share;

namespace NoPrint.Application.Ef.Repositories;

public class CustomerRepository : RepositoryBase<Customer,CustomerId>, ICustomerRepository
{
    public CustomerRepository(NoPrintContext context) : base(context)
    {

    }
}