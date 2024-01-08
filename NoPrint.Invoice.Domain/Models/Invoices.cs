using System.Diagnostics.CodeAnalysis;
using NoPrint.Framework.Exceptions;
using NoPrint.Framework.Specification;
using NoPrint.Framework.Validation;
using NoPrint.Invoice.Domain.Specification;

namespace NoPrint.Invoice.Domain.Models;

public class Invoices
{
    private Invoices() { }

    public long InvoiceId { get; private set; }
    public long ShopId { get; private set; }
    public string CustomerNumber { get; private set; }
    private List<InvoiceItem> Items { get; set; }
    public IReadOnlyList<InvoiceItem> InvoiceItems => Items.AsReadOnly();
    public decimal RawCost { get; private set; }
    public decimal DiscountRate { get; private set; }
    public decimal DiscountFee { get; private set; }
    public decimal FinalCost { get; private set; }

    private void SetItems(List<InvoiceItem> items)
    {
        items.ValidationCheck(nameof(InvoiceItems),x => x?.Any() == true && x.All(x => x is not null) , "E1009");
        items.ValidationCheck(nameof(InvoiceItems),x => DiscountFee == x.Sum(y => y.DiscountFee), "E1013");
        items.ValidationCheck(nameof(InvoiceItems),x => FinalCost == x.Sum(y => y.FinalFee), "E1014");
        items.ValidationCheck(nameof(InvoiceItems),x => RawCost == x.Sum(y => y.RawFee), "E1012");

        Items = items;
    }

    public static Invoices Create(string customerNumber, decimal rawCost, decimal discountRate, decimal discountFee, decimal finalCost, List<InvoiceItem> invoiceItems)
    {
        customerNumber.ValidationCheck(nameof(CustomerNumber), x => x.Length == 11 && x.StartsWith('0'), "E1011");
        rawCost.ValidationCheck(nameof(RawCost), x=> x > 0, "E1020");
        discountRate.ValidationCheck(nameof(DiscountRate), x => x is >= 0 and <= 100 && x * rawCost == discountFee , "E1015");
        discountFee.ValidationCheck(nameof(DiscountFee), x=> x > 0, "E1021");
        finalCost.ValidationCheck(nameof(FinalCost), x=> x > 0 && x == rawCost -  discountFee, "E1022");

        
        var invoice = new Invoices()
        {
            CustomerNumber = customerNumber,
            RawCost = rawCost,
            DiscountRate = discountRate,
            DiscountFee = discountFee,
            FinalCost = finalCost,
        };

        invoice.SetItems(invoiceItems);

        return invoice;
    }
}