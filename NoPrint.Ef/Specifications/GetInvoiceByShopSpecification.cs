using Microsoft.EntityFrameworkCore;
using NoPrint.Framework.Specification;
using NoPrint.Framework.Validation;
using NoPrint.Identity.Share;
using NoPrint.Invoices.Domain.Models;

namespace NoPrint.Application.Ef.Specifications;

public class GetInvoiceByShopSpecification : IBaseGetListSpecification<Invoice>
{
    private readonly ShopId _shopId;

    public GetInvoiceByShopSpecification(ShopId shopId)
    {
        shopId.ValidationCheck("Shop", x => x is not null, "E1035");
        _shopId = shopId;
    }

    public async Task<List<Invoice>> GetAllAsync(IQueryable<Invoice> queryable)
    {
        return await queryable.AsNoTracking().Where(x => x.Shop.Id == _shopId.Id).ToListAsync();
    }
}