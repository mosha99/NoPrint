using MediatR;
using NoPrint.Application.Infra;
using NoPrint.Framework;

namespace NoPrint.Application.CommandsAndQueries.Customer.Commands;

[Access(Rule.NonAuthorize)]
public class GetTokenForCustomerByUserNameCommand : IRequest<TokenBehavior>
{
    public string UserName { get; set; }
    public string Password { get; set; }
}