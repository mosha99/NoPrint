namespace NoPrint.Application.CommandsAndQueries.Invoices.Querys;

public abstract class ListFilterBase
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}