using Microsoft.EntityFrameworkCore;
using NoPrint.Framework.Specification;
using NoPrint.Framework.Validation;
using Noprint.Identity.Share;
using NoPrint.Invoices.Domain.Models;

namespace NoPrint.Ef.Specifications;

public class GetInvoiceByCustomerSpecification : IBaseGetListSpecification<Invoice>
{
    private readonly CustomerId _customerId;

    public GetInvoiceByCustomerSpecification(CustomerId customerId)
    {
        customerId.ValidationCheck("Customer", x => x is not null, "E1035");
        _customerId = customerId;
    }

    public async Task<List<Invoice>> GetAllAsync(IQueryable<Invoice> queryable)
    {
        return await queryable.AsNoTracking().Where(x => x.Customer.Id == _customerId.Id).ToListAsync();
    }
}