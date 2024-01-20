using Microsoft.EntityFrameworkCore;
using NoPrint.Application.CommandsAndQueries.Invoices.Querys;
using NoPrint.Application.Infra;
using NoPrint.Customers.Domain.Model;
using NoPrint.Customers.Domain.Repository;
using NoPrint.Framework.Specification;
using NoPrint.Framework.Validation;
using NoPrint.Identity.Share;
using NoPrint.Invoices.Domain.Models;
using NoPrint.Shops.Domain.Repository;

namespace NoPrint.Application.Ef.Specifications;

public class GetInvoiceByCustomerSpecification : IBaseGetListSpecification<Invoice>
{
    private readonly CustomerId _customerId;

    public GetInvoiceByCustomerSpecification(CustomerId customerId)
    {
        customerId.ValidationCheck("Customer", x => x is not null, "Error_NotFind");
        _customerId = customerId;
    }

    public async Task<ListResult<Invoice>> GetAllAsync(IQueryable<Invoice> queryable)
    {
        return new(await queryable.AsNoTracking().Where(x => x.Customer.Id == _customerId.Id).ToListAsync());
    }
}
public class GetInvoiceForCustomerQuerySpecification : IBaseGetListSpecification<Invoice>
{
    private readonly CustomerId _customerId;
    private readonly GetInvoiceForCustomerQuery _customerQuery;
    private readonly ICustomerRepository _customerRepository;
    private readonly IShopRepository _shopRepository;

    public GetInvoiceForCustomerQuerySpecification(
        CustomerId customerId,
        GetInvoiceForCustomerQuery customerQuery,
        ICustomerRepository customerRepository,
        IShopRepository shopRepository)
    {
        _customerId = customerId;
        _customerQuery = customerQuery;
        _customerRepository = customerRepository;
        _shopRepository = shopRepository;
    }

    public async Task<ListResult<Invoice>> GetAllAsync(IQueryable<Invoice> queryable)
    {
        try
        {

            queryable = queryable.Where(x => x.Shop.Id == _customerId.Id);

            var query = _customerQuery.Effect(queryable);

            query = query.Where(x => x.Items.Any(x => _customerQuery.ProductName == null || x.ProductName.Contains(_customerQuery.ProductName)));



            IEnumerable<long> shopsId = (await _shopRepository.GetAllByCondition(new GetShopsByNameSpecification(_customerQuery.ShopName))).List.Select(x => x._Id);

            if (_customerQuery.ShopName is not null)
            {
                query = query.Where(x => shopsId.Contains(x.Shop.Id));
            }

            var result = new ListResult<Invoice>();

            var s = query.ToQueryString();

            result.TotalCount = await query.CountAsync();

            query.Skip(_customerQuery.PageSize * (_customerQuery.PageNumber - 1))
                .Take(_customerQuery.PageSize);

            result.List = await query.ToListAsync();

            return result;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

}
public class GetInvoiceForShopQuerySpecification : IBaseGetListSpecification<Invoice>
{
    private readonly ShopId _shopId;
    private readonly GetInvoiceForShopQuery _shopQuery;
    private readonly ICustomerRepository _customerRepository;
    private readonly IShopRepository _shopRepository;

    public GetInvoiceForShopQuerySpecification(
        ShopId  shopId,
        GetInvoiceForShopQuery shopQuery,
        ICustomerRepository customerRepository,
        IShopRepository shopRepository)
    {
        _shopId = shopId;
        _shopQuery = shopQuery;
        _customerRepository = customerRepository;
        _shopRepository = shopRepository;
    }

    public async Task<ListResult<Invoice>> GetAllAsync(IQueryable<Invoice> queryable)
    {
        try
        {
            queryable = queryable.Where(x => x.Shop.Id == _shopId.Id);

            var query = _shopQuery.Effect(queryable);

            IEnumerable<long> customerId = (await _customerRepository.GetAllByCondition(new GetCustomerByPartOfPhoneSpecification(_shopQuery.CustomerPhone))).List.Select(x => x._Id);
            if (_shopQuery.CustomerPhone is not null)
            {
                query = query.Where(x => customerId.Contains(x.Shop.Id));
            }

            var result = new ListResult<Invoice>();

            result.TotalCount = await query.CountAsync();

            query.Skip(_shopQuery.PageSize * (_shopQuery.PageNumber - 1))
                .Take(_shopQuery.PageSize);

            result.List = await query.ToListAsync();

            return result;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


}