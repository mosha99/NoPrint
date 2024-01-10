using Noprint.Identity.Share;
using NoPrint.Framework;
using NoPrint.Shops.Domain.Models;

namespace NoPrint.Shops.Domain.Repository;

public interface IShopRepository : IRepositoryBase<Shop, ShopId>
{
}