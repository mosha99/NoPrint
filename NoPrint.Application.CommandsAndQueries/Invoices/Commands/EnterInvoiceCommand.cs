using MediatR;
using NoPrint.Application.Dto;
using NoPrint.Application.Infra;
using NoPrint.Framework;
using NoPrint.Identity.Share;
using NoPrint.Invoices.Domain.ValueObjects;

namespace NoPrint.Application.CommandsAndQueries.Invoices.Commands;
[Access(Rule.Shop_User)]
public class EnterInvoiceCommand : IRequest<long>
{
    public string CustomerPhoneNumber { set; get; }
    public decimal RawCost{ set; get; }
    public decimal DiscountRate{ set; get; }
    public decimal DiscountFee{ set; get; }
    public decimal FinalCost{ set; get; }

    public List<InvoiceItemDto> InvoiceItems{ set; get; }
}