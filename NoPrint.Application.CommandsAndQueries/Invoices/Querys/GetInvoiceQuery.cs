using MediatR;
using NoPrint.Identity.Share;
using NoPrint.Invoices.Domain.Models;

namespace NoPrint.Application.CommandsAndQueries.Invoices.Querys;

public class GetInvoiceQuery : IRequest<Invoice>
{
    public long InvoicesId { get; set; }
    public InvoicesId GetInvoicesId() => new InvoicesId() { Id = InvoicesId };
}