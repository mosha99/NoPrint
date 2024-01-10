using Microsoft.EntityFrameworkCore;
using Noprint.Identity.Share;
using NoPrint.Ef.Base;
using NoPrint.Shops.Domain.Models;
using NoPrint.Shops.Domain.Repository;

namespace NoPrint.Ef.Repositories;

public class ShopRepository : RepositoryBase<Shop, ShopId>, IShopRepository
{
    public ShopRepository(NoPrintContext context) : base(context)
    {

    }
}