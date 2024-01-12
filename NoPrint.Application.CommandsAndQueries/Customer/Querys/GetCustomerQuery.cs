using MediatR;
using NoPrint.Identity.Share;

namespace NoPrint.Application.CommandsAndQueries.Customer.Querys;

public class GetCustomerQuery : IRequest<Shops.Domain.Models.Shop>
{
    public long CustomerId { get; set; }
    public CustomerId GetCustomerId() => new CustomerId() { Id = CustomerId };
}