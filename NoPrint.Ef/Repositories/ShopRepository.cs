using NoPrint.Application.Ef.Base;
using NoPrint.Framework.Validation;
using NoPrint.Identity.Share;
using NoPrint.Shops.Domain.Models;
using NoPrint.Shops.Domain.Repository;

namespace NoPrint.Application.Ef.Repositories;

public class ShopRepository : RepositoryBase<Shop, ShopId>, IShopRepository
{
    public ShopRepository(NoPrintContext context) : base(context)
    {

    }

    public override Task<ShopId> AddAsync(Shop entity)
    {
        _context.Set<Shop>().Any(x=>x.PhoneNumber.Equals(entity.PhoneNumber))
            .ValidationCheck(nameof(Shop.PhoneNumber),x=>!x, "Error_PhoneIsUniq");

        return base.AddAsync(entity);
    }
}