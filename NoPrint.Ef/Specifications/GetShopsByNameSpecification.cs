using Microsoft.EntityFrameworkCore;
using NoPrint.Application.Infra;
using NoPrint.Framework.Specification;
using NoPrint.Shops.Domain.Models;

namespace NoPrint.Application.Ef.Specifications;

public class GetShopsByNameSpecification : IBaseGetListSpecification<Shop>
{
    private readonly string _name;

    public GetShopsByNameSpecification(string name)
    {
        _name = name;
    }

    public async Task<ListResult<Shop>> GetAllAsync(IQueryable<Shop> queryable)
    {
        return new(await queryable.Where(x => x.ShopName.Contains(_name)).ToListAsync()) ;
    }
}