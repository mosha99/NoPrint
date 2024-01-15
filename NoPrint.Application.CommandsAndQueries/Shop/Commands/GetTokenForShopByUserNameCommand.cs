using MediatR;
using NoPrint.Application.Infra;
using NoPrint.Framework;

namespace NoPrint.Application.CommandsAndQueries.Shop.Commands;
[Access(Rule.NonAuthorize)]

public class GetTokenForShopByUserNameCommand : IRequest<TokenBehavior>
{
    public string UserName { get; set; }
    public string Password { get; set; }
}