using Microsoft.EntityFrameworkCore;
using Noprint.Identity.Share;
using NoPrint.Invoices.Domain.Models;
using NoPrint.Invoices.Domain.Repository;

namespace NoPrint.Ef.Repositories;

public class InvoiceRepository : RepositoryBase<Invoice, InvoicesId>, IInvoicesRepository
{
    public InvoiceRepository(DbContext context) : base(context)
    {

    }
}