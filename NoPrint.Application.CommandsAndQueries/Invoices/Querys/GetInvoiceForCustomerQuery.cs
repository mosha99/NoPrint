using MediatR;
using NoPrint.Application.Dto;
using NoPrint.Application.Infra;
using NoPrint.Invoices.Domain.Models;

namespace NoPrint.Application.CommandsAndQueries.Invoices.Querys;

[Access(Rule.Customer_User,Rule.Customer_Visitor)]
public class GetInvoiceForCustomerQuery : GetInvoiceBase, IRequest<ListResult<InvoiceDto>>
{
    public string? ShopName { get; set; }
}