using MediatR;
using NoPrint.Application.Dto;
using NoPrint.Application.Infra;
using NoPrint.Invoices.Domain.Models;

namespace NoPrint.Application.CommandsAndQueries.Invoices.Querys;
[Access(Rule.Shop_User)]
public class GetInvoiceForShopQuery : GetInvoiceBase,IRequest<ListResult<InvoiceDto>> 
{
    public string? CustomerPhone { get; set; }
}