using NoPrint.Framework.Validation;

namespace NoPrint.Invoices.Domain.ValueObjects;

public class InvoiceItem
{
    private InvoiceItem() { }

    public static InvoiceItem CreateInstance(string productName, string productCode, decimal count, decimal price, decimal rawFee, decimal discountRate, decimal discountFee, decimal finalFee)
    {
        productName.ValidationCheck(nameof(ProductName), x => !string.IsNullOrWhiteSpace(x), "E1001");
        productCode.ValidationCheck(nameof(ProductCode), x => !string.IsNullOrWhiteSpace(x), "E1002");
        count.ValidationCheck(nameof(Count), x => x > 0, "E1003");
        price.ValidationCheck(nameof(Price), x => x > 0, "E1004");
        rawFee.ValidationCheck(nameof(RawFee), x => x > 0 && x == count * price, "E1005");
        discountRate.ValidationCheck(nameof(DiscountRate), x => x >= 0, "E1006");
        discountFee.ValidationCheck(nameof(DiscountFee), x => x >= 0 && x == rawFee * discountRate, "E1007");
        finalFee.ValidationCheck(nameof(FinalFee), x => x == rawFee - discountFee, "E1008");

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