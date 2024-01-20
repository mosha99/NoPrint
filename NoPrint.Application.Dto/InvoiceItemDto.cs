using NoPrint.Invoices.Domain.ValueObjects;

namespace NoPrint.Application.Dto
{
    public class InvoiceItemDto 
    {
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public decimal Count { get; set; }
        public decimal Price { get; set; }
        public decimal RawFee { get; set; }
        public decimal DiscountRate { get; set; }
        public decimal DiscountFee { get; set; }
        public decimal FinalFee { get; set; }

        public InvoiceItem ToModel()
        {
            return InvoiceItem.CreateInstance(ProductName, ProductCode, Count, Price, RawFee, DiscountRate, DiscountFee, FinalFee);
        }

        public static InvoiceItemDto FromModel(InvoiceItem model)
        {
            var itemDto= new InvoiceItemDto
            {
                ProductName = model.ProductName,
                ProductCode = model.ProductCode,
                Count = model.Count,
                Price = model.Price,
                RawFee = model.RawFee,
                DiscountRate = model.DiscountRate,
                DiscountFee = model.DiscountFee,
                FinalFee = model.FinalFee
            };

            return itemDto;
        }
    }
}
