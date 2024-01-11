using System.Security.Principal;
using NoPrint.Customers.Domain.Model;
using NoPrint.Framework;
using NoPrint.Identity.Share;

namespace NoPrint.Customers.Domain.Repository;

public interface ICustomerRepository : IRepositoryBase<Customer,CustomerId>
{

}