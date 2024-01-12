﻿using MediatR;
using NoPrint.Identity.Share;
using NoPrint.Invoices.Domain.ValueObjects;

namespace NoPrint.Application.CommandsAndQueries.Invoices.Commands;

public class EnterInvoiceCommand : IRequest<long>
{
    public ShopId GetShopId() => new ShopId(){Id = ShopId};
    public long ShopId { set; get; }
    public string CustomerPhoneNumber { set; get; }
    public decimal RawCost{ set; get; }
    public decimal DiscountRate{ set; get; }
    public decimal DiscountFee{ set; get; }
    public decimal FinalCost{ set; get; }

    public List<InvoiceItem> InvoiceItems{ set; get; }
}