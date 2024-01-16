using NoPrint.Framework.Validation;

namespace NoPrint.Invoices.Domain.ValueObjects;

public class InvoiceItem
{
    private InvoiceItem() { }

    public static InvoiceItem CreateInstance(string productName, string productCode, decimal count, decimal price, decimal rawFee, decimal discountRate, decimal discountFee, decimal finalFee)
    {
        productName.ValidationCheck(nameof(ProductName), x => !string.IsNullOrWhiteSpace(x), "Error_Required");
        productCode.ValidationCheck(nameof(ProductCode), x => !string.IsNullOrWhiteSpace(x), "Error_Required");
        count.ValidationCheck(nameof(Count), x => x > 0, "Error_NotValid");
        price.ValidationCheck(nameof(Price), x => x > 0, "Error_NotValid");
        rawFee.ValidationCheck(nameof(RawFee), x => x > 0 && x == count * price, "Error_NotValid");
        discountRate.ValidationCheck(nameof(DiscountRate), x => x >= 0, "Error_NotValid");
        discountFee.ValidationCheck(nameof(DiscountFee), x => x >= 0 && x == rawFee * discountRate, "Error_NotValid");
        finalFee.ValidationCheck(nameof(FinalFee), x => x == rawFee - discountFee, "Error_NotValid");

        return new InvoiceItem()
        {
            ProductName = productName,
            ProductCode = productCode,
            Count = count,
            Price = price,
            RawFee = rawFee,
            DiscountRate = discountRate,
            DiscountFee = discountFee,
            FinalFee = finalFee
        };
    }

    public string ProductName { get; private set; }
    public string ProductCode { get; private set; }
    public decimal Count { get; private set; }
    public decimal Price { get; private set; }
    public decimal RawFee { get; private set; }
    public decimal DiscountRate { get; private set; }
    public decimal DiscountFee { get; private set; }
    public decimal FinalFee { get; private set; }
}