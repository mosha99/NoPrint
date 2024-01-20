using NoPrint.Identity.Share;
using NoPrint.Invoices.Domain.Models;

namespace NoPrint.Application.CommandsAndQueries.Invoices.Querys;

public abstract class GetInvoiceBase : ListFilterBase
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }

    public decimal? FromAmount { get; set; }
    public decimal? ToAmount { get; set; }

    public string? ProductName { get; set; }

    public IQueryable<Invoice> Effect(IQueryable<Invoice> queryable)
    {
        var query = queryable.Where(x =>
            (this.FromAmount == null || x.FinalCost >= this.FromAmount) &&
            (this.ToAmount == null || x.FinalCost <= this.ToAmount) &&
            (this.FromDate == null || x.EnterDateTime >= this.FromDate) &&
            (this.ToDate == null || x.EnterDateTime <= this.ToDate));


        query = query.Where(x =>
            x.Items.Any(x => this.ProductName == null || x.ProductName.Contains(this.ProductName)));
        return query;
    }
}