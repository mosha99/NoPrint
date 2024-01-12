using MediatR;
using NoPrint.Identity.Share;

namespace NoPrint.Application.CommandsAndQueries.Shop.Querys;

public class GetShopQuery : IRequest<Shops.Domain.Models.Shop>
{
    public long ShopId { get; set; }
    public ShopId GetShopId() => new ShopId() { Id = ShopId };
}