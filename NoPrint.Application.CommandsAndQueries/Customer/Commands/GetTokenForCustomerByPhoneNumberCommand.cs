using MediatR;
using NoPrint.Application.Infra;
using NoPrint.Framework;

namespace NoPrint.Application.CommandsAndQueries.Customer.Commands;
[Access(Rule.NonAuthorize)]

public class GetTokenForCustomerByPhoneNumberCommand : IRequest<TokenBehavior>
{
    public string PhoneNumber { get; set; }
    public string Code { get; set; }
}