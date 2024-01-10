using NoPrint.Framework.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using NoPrint.Framework;
using Noprint.Identity.Share;
using NoPrint.Invoices.Domain.ValueObjects;

namespace NoPrint.Invoices.Domain.Models;

public class Invoice : Aggregate<InvoicesId>
{
    private Invoice() { }

    public long InvoiceId { get; private set; }
    public ShopId Shop { get; private set; }
    public CustomerId Customer { get; private set; }
    private List<InvoiceItem> Items { get; set; }
    [NotMapped]
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

    public static Invoice Create(CustomerId customerId,ShopId shopId, decimal rawCost, decimal discountRate, decimal discountFee, decimal finalCost, List<InvoiceItem> invoiceItems)
    {
        shopId.ValidationCheck(nameof(Shop), x => x.Id != 0 , "E1023");
        customerId.ValidationCheck(nameof(Customer), x => x.Id != 0 , "E1011");
        rawCost.ValidationCheck(nameof(RawCost), x=> x > 0, "E1020");
        discountRate.ValidationCheck(nameof(DiscountRate), x => x is >= 0 and <= 100 && x * rawCost == discountFee , "E1015");
        discountFee.ValidationCheck(nameof(DiscountFee), x=> x > 0, "E1021");
        finalCost.ValidationCheck(nameof(FinalCost), x=> x > 0 && x == rawCost -  discountFee, "E1022");

        
        var invoice = new Invoice()
        {
            Customer = customerId,
            Shop = shopId,
            RawCost = rawCost,
            DiscountRate = discountRate,
            DiscountFee = discountFee,
            FinalCost = finalCost,
        };

        invoice.SetItems(invoiceItems);

        return invoice;
    }
}