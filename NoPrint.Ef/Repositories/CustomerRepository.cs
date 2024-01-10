using Microsoft.EntityFrameworkCore;
using NoPrint.Customers.Domain.Model;
using NoPrint.Customers.Domain.Repository;
using Noprint.Identity.Share;
using NoPrint.Ef.Base;

namespace NoPrint.Ef.Repositories;

public class CustomerRepository : RepositoryBase<Customer,CustomerId>, ICustomerRepository
{
    public CustomerRepository(NoPrintContext context) : base(context)
    {

    }

}