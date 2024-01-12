using MediatR;

namespace NoPrint.Application.CommandsAndQueries.Customer.Commands;

public class GetTokenForCustomerByPhoneNumberCommand : IRequest<string>
{
    public string PhoneNumber { get; set; }
    public string Code { get; set; }
}