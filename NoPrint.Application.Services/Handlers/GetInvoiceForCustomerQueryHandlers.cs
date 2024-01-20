using MediatR;
using NoPrint.Application.CommandsAndQueries.Invoices.Querys;
using NoPrint.Application.Dto;
using NoPrint.Application.Ef.Specifications;
using NoPrint.Application.Infra;
using NoPrint.Customers.Domain.Repository;
using NoPrint.Identity.Share;
using NoPrint.Invoices.Domain.Repository;
using NoPrint.Shops.Domain.Repository;

namespace NoPrint.Application.Services.Handlers;

public class GetInvoiceForCustomerQueryHandlers : IRequestHandler<GetInvoiceForCustomerQuery, ListResult<InvoiceDto>>
{
    private readonly IInvoicesRepository _invoicesRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IShopRepository _shopRepository;
    private readonly IIdentityStorageService _identityStorageService;


    public GetInvoiceForCustomerQueryHandlers(
        IInvoicesRepository invoicesRepository,
        ICustomerRepository customerRepository,
        IShopRepository shopRepository,
        IIdentityStorageService identityStorageService)
    {
        _invoicesRepository = invoicesRepository;
        _customerRepository = customerRepository;
        _shopRepository = shopRepository;
        _identityStorageService = identityStorageService;
    }

    public async Task<ListResult<InvoiceDto>> Handle(GetInvoiceForCustomerQuery customerQuery, CancellationToken cancellationToken)
    {
        var currentUserId = _identityStorageService.GetIdentityItem<UserId>("UserId");

        var customer = await _customerRepository.GetSingleByCondition(new GetCustomerByUserSpecification(currentUserId));

        var invoices = await _invoicesRepository
            .GetAllByCondition(new GetInvoiceForCustomerQuerySpecification(customer.Id, customerQuery, _customerRepository, _shopRepository));

        ListResult<InvoiceDto> result = new();

        result.TotalCount = invoices.TotalCount;
        result.List = new();

        foreach (var invoice in invoices.List)
        {
            result.List.Add(await InvoiceDto.FromModel(invoice, _customerRepository, _shopRepository));
        }

        return result;
    }
}