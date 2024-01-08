using NoPrint.Framework.Specification;
using NoPrint.Invoice.Domain.Models;

namespace NoPrint.Invoice.Domain.Specification;

public class InvoiceItemsValidationSpecification : ISpecification
{
    private readonly IEnumerable<InvoiceItem> _items;

    public InvoiceItemsValidationSpecification(IEnumerable<InvoiceItem> items)
    {
        _items = items;
    }

    public bool Satisfied()
    {
        if (_items?.Any() != true) return false;

        foreach (var invoiceItem in _items)
        {
            if (invoiceItem.Count * invoiceItem.Price != invoiceItem.RawFee) return false;
            if (invoiceItem.RawFee * invoiceItem.DiscountRate != invoiceItem.DiscountFee) return false;
            if (invoiceItem.RawFee - invoiceItem.FinalFee != invoiceItem.DiscountFee) return false;
        }

        return true;
    }
}