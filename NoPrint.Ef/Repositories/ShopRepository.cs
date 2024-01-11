using Microsoft.EntityFrameworkCore;
using NoPrint.Ef.Base;
using NoPrint.Identity.Share;
using NoPrint.Shops.Domain.Models;
using NoPrint.Shops.Domain.Repository;

namespace NoPrint.Ef.Repositories;

public class ShopRepository : RepositoryBase<Shop, ShopId>, IShopRepository
{
    public ShopRepository(NoPrintContext context) : base(context)
    {

    }
}