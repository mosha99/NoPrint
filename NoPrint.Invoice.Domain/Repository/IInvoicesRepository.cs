using NoPrint.Framework.Specification;
using Noprint.Identity.Share;
using NoPrint.Invoices.Domain.Models;
using NoPrint.Framework;

namespace NoPrint.Invoices.Domain.Repository;

public interface IInvoicesRepository : IRepositoryBase<Invoice,InvoicesId>
{
}