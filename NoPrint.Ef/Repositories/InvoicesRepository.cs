using NoPrint.Application.Ef.Base;
using NoPrint.Identity.Share;
using NoPrint.Invoices.Domain.Models;
using NoPrint.Invoices.Domain.Repository;

namespace NoPrint.Application.Ef.Repositories;

public class InvoicesRepository : RepositoryBase<Invoice,InvoicesId>, IInvoicesRepository
{
    public InvoicesRepository(NoPrintContext context) : base(context)
    {

    }

}