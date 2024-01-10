using Microsoft.EntityFrameworkCore;
using NoPrint.Customers.Domain.Model;
using NoPrint.Customers.Domain.Repository;
using Noprint.Identity.Share;

namespace NoPrint.Ef.Repositories;

public class CustomerRepository : RepositoryBase<Customer,CustomerId>, ICustomerRepository
{
    public CustomerRepository(DbContext context) : base(context)
    {

    }

}