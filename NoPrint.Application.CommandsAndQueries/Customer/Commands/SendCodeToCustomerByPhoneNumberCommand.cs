using MediatR;
using NoPrint.Application.Infra;
using NoPrint.Framework;

namespace NoPrint.Application.CommandsAndQueries.Customer.Commands;
[Access(Rule.NonAuthorize)]

public class SendCodeToCustomerByPhoneNumberCommand : IRequest
{
    public string PhoneNumber { get; set; }
}