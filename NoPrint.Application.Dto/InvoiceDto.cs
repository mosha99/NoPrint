using NoPrint.Identity.Share;
using NoPrint.Invoices.Domain.Models;
using NoPrint.Invoices.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;
using NoPrint.Customers.Domain.Repository;
using NoPrint.Shops.Domain.Repository;

namespace NoPrint.Application.Dto;

public class InvoiceDto 
{

    public long InvoiceId { get; private set; }

    public string ShopName { get; private set; }
    public string CustomerName { get; private set; }
    public string CustomerPhoneNumber { get; private set; }

    public List<InvoiceItemDto> Items { get; set; }
    public decimal RawCost { get; private set; }
    public decimal DiscountRate { get; private set; }
    public decimal DiscountFee { get; private set; }
    public decimal FinalCost { get; private set; }


    public static async Task<InvoiceDto> FromModel(Invoice model , ICustomerRepository customerRepository , IShopRepository shopRepository)
    {
        var customer = await customerRepository.GetByIdAsync(model.Customer);
        var shop = await shopRepository.GetByIdAsync(model.Shop);

        return new InvoiceDto()
        {
            DiscountFee = model.DiscountFee,
            DiscountRate = model.DiscountRate,
            RawCost = model.RawCost,
            FinalCost = model.FinalCost,
            Items = model.Items.Select(InvoiceItemDto.FromModel).ToList(),
            InvoiceId = model._Id,
            CustomerName = customer.CustomerName,
            CustomerPhoneNumber = customer.PhoneNumber,
            ShopName = shop.ShopName,
        };
    }
}