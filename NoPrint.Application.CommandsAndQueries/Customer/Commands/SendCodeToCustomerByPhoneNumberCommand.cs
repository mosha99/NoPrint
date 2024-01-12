using MediatR;

namespace NoPrint.Application.CommandsAndQueries.Customer.Commands;

public class SendCodeToCustomerByPhoneNumberCommand : IRequest
{
    public string PhoneNumber { get; set; }
}