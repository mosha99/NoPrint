using MediatR;
using NoPrint.Application.Infra;
using NoPrint.Framework;
using NoPrint.Identity.Share;

namespace NoPrint.Application.CommandsAndQueries.Customer.Commands;
[Access(Rule.Customer_Visitor)]
public class FillCustomerCommand : IRequest<long>
{
    public long CustomerId { get; set; }
    public CustomerId GetCustomerId() => new CustomerId() { Id = CustomerId };
    public string UserName { get; set; }
    public string Password { get; set; }
    public string CustomerAddress { get; set; }
    public string CustomerName { get; set; }

}