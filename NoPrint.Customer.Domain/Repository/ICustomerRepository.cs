using System.Security.Principal;
using Noprint.Identity.Share;
using NoPrint.Customers.Domain.Model;
using NoPrint.Framework;

namespace NoPrint.Customers.Domain.Repository;

public interface ICustomerRepository : IRepositoryBase<Customer,CustomerId>
{

}