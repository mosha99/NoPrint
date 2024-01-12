using MediatR;

namespace NoPrint.Application.CommandsAndQueries.Customer.Commands;

public class GetTokenForCustomerByUserNameCommand : IRequest<string>
{
    public string UserName { get; set; }
    public string Password { get; set; }
}