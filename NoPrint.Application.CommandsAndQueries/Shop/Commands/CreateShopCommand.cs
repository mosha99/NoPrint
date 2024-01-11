using MediatR;

namespace NoPrint.Application.CommandsAndQueries.Shop.Commands;

public class CreateShopCommand : IRequest<long>
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string ShopName { get; set; }
    public string PhoneNumber { get; set; }
    public string ShopAddress { get; set; }
}