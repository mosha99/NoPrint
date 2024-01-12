using Microsoft.EntityFrameworkCore;
using NoPrint.Customers.Domain.Model;
using NoPrint.Framework.Specification;
using NoPrint.Identity.Share;

namespace NoPrint.Ef.Specifications;

public class GetCustomerByUserSpecification : IBaseGetSpecification<Customer>
{
    private readonly UserId _userId;


    public GetCustomerByUserSpecification(UserId userId)
    {
        _userId = userId;
    }

    public async Task<Customer> GetAsync(IQueryable<Customer> queryable)
    {
        return await queryable.SingleAsync(x => x.User.Id == _userId.Id);
    }
}