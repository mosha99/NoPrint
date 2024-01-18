using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NoPrint.Application.Infra;
using NoPrint.Framework;

namespace NoPrint.Application.CommandsAndQueries.Shop.Commands;

[Access(Rule.NonAuthorize)]
public class CreateShopCommand : IRequest<long>
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string ShopName { get; set; }
    public string PhoneNumber { get; set; }
    public string ShopAddress { get; set; }
}

