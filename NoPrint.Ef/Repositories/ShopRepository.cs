using NoPrint.Application.Ef.Base;
using NoPrint.Identity.Share;
using NoPrint.Shops.Domain.Models;
using NoPrint.Shops.Domain.Repository;

namespace NoPrint.Application.Ef.Repositories;

public class ShopRepository : RepositoryBase<Shop, ShopId>, IShopRepository
{
    public ShopRepository(NoPrintContext context) : base(context)
    {

    }
}