using NoPrint.Application.Ef.Base;
using NoPrint.Identity.Share;
using NoPrint.Invoices.Domain.Models;
using NoPrint.Invoices.Domain.Repository;

namespace NoPrint.Application.Ef.Repositories;

public class InvoiceRepository : RepositoryBase<Invoice, InvoicesId>, IInvoicesRepository
{
    public InvoiceRepository(NoPrintContext context) : base(context)
    {

    }
}