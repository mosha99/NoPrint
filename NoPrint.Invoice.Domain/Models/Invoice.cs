using NoPrint.Framework.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using NoPrint.Framework;
using NoPrint.Identity.Share;
using NoPrint.Invoices.Domain.ValueObjects;

namespace NoPrint.Invoices.Domain.Models;

public class Invoice : Aggregate<InvoicesId>
{
    private Invoice() { }

    public ShopId Shop { get; private set; }
    public CustomerId Customer { get; private set; }
   // private List<InvoiceItem> Items { get; set; }
    //[NotMapped]
    public IReadOnlyList<InvoiceItem> Items { get; private set; }
    public decimal RawCost { get; private set; }
    public decimal DiscountRate { get; private set; }
    public decimal DiscountFee { get; private set; }
    public decimal FinalCost { get; private set; }

    public DateTime EnterDateTime { get; private set; }
    private void SetItems(List<InvoiceItem> items)
    {
        items.ValidationCheck(nameof(Items),x => x?.Any() == true && x.All(x => x is not null) , "Error_InvoiceItemNotValid");
        items.ValidationCheck(nameof(Items),x => DiscountFee == x.Sum(y => y.DiscountFee), "Error_InvoiceItemNotValid");
        items.ValidationCheck(nameof(Items),x => FinalCost == x.Sum(y => y.FinalFee), "Error_InvoiceItemNotValid");
        items.ValidationCheck(nameof(Items),x => RawCost == x.Sum(y => y.RawFee), "Error_InvoiceItemNotValid");

        Items = items;
    }

    public static Invoice Create(CustomerId customerId,ShopId shopId, decimal rawCost, decimal discountRate, decimal discountFee, decimal finalCost, List<InvoiceItem> invoiceItems)
    {   
        discountRate.ValidationCheck(nameof(DiscountRate), x => x is >= 0 and <= 100 && x * rawCost == discountFee , "Error_NotValid");
        discountFee.ValidationCheck(nameof(DiscountFee), x=> x >= 0, "Error_NotValid");
        customerId.ValidationCheck(nameof(Customer), x => x.Id != 0 , "Error_Required");
        finalCost.ValidationCheck(nameof(FinalCost), x=> x > 0 && x == rawCost -  discountFee, "Error_NotValid");
        rawCost.ValidationCheck(nameof(RawCost), x=> x > 0, "Error_NotValid");
        shopId.ValidationCheck(nameof(Shop), x => x.Id != 0 , "Error_Required");

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