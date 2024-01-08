using NoPrint.Framework.Specification;
using NoPrint.Invoice.Domain.Models;

namespace NoPrint.Invoice.Domain.Specification;

public class InvoiceRawCostMachWithInvoiceItems : ISpecification
{
    private readonly Invoices _invoices;
    private readonly IEnumerable<InvoiceItem> _items;

    public InvoiceRawCostMachWithInvoiceItems(Invoices invoices, IEnumerable<InvoiceItem> items)
    {
        _invoices = invoices;
        _items = items;
    }

    public bool Satisfied()
    {
        return _items.Sum(x => x.RawFee).Equals(_invoices.RawCost);
    }
}