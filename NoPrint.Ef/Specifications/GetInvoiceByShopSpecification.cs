using Microsoft.EntityFrameworkCore;
using NoPrint.Application.Infra;
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
        shopId.ValidationCheck("Shop", x => x is not null, "Error_NotFind");
        _shopId = shopId;
    }

    public async Task<ListResult<Invoice>> GetAllAsync(IQueryable<Invoice> queryable)
    {
        return new (await queryable.AsNoTracking().Where(x => x.Shop.Id == _shopId.Id).ToListAsync());
    }
}