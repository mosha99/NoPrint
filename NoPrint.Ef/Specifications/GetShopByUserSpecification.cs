using Microsoft.EntityFrameworkCore;
using NoPrint.Framework.Specification;
using NoPrint.Identity.Share;
using NoPrint.Shops.Domain.Models;

namespace NoPrint.Application.Ef.Specifications;

public class GetShopByUserSpecification : IBaseGetSpecification<Shop>
{
    private readonly UserId _userId;


    public GetShopByUserSpecification(UserId userId)
    {
        _userId = userId;
    }

    public async Task<Shop> GetAsync(IQueryable<Shop> queryable)
    {
        return await queryable.SingleAsync(x => x.User.Id == _userId.Id);
    }
}