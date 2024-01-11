using NoPrint.Framework.Specification;
using NoPrint.Invoices.Domain.Models;
using NoPrint.Framework;
using NoPrint.Identity.Share;

namespace NoPrint.Invoices.Domain.Repository;

public interface IInvoicesRepository : IRepositoryBase<Invoice,InvoicesId>
{
}