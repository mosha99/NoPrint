using Microsoft.EntityFrameworkCore;
using NoPrint.Customers.Domain.Model;
using NoPrint.Customers.Domain.Repository;
using NoPrint.Ef.Base;
using NoPrint.Identity.Share;

namespace NoPrint.Ef.Repositories;

public class CustomerRepository : RepositoryBase<Customer,CustomerId>, ICustomerRepository
{
    public CustomerRepository(NoPrintContext context) : base(context)
    {

    }
}