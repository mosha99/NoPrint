using MediatR;
using NoPrint.Application.CommandsAndQueries.Customer.Notifications;
using NoPrint.Application.CommandsAndQueries.Invoices.Commands;
using NoPrint.Application.Ef;
using NoPrint.Application.Ef.Repositories;
using NoPrint.Application.Ef.Specifications;
using NoPrint.Application.Infra;
using NoPrint.Customers.Domain.Model;
using NoPrint.Customers.Domain.Repository;
using NoPrint.Ef;
using NoPrint.Identity.Share;
using NoPrint.Invoices.Domain.Models;
using NoPrint.Invoices.Domain.Repository;
using NoPrint.Shops.Domain.Repository;
using NoPrint.Users.Domain.Models;
using NoPrint.Users.Domain.Repository;
using NoPrint.Users.Domain.ValueObjects;

namespace NoPrint.Application.Services.Handlers;

public class EnterInvoiceCommandHandlers : IRequestHandler<EnterInvoiceCommand, long>
{
    private readonly UnitRepositories _repositories;
    private readonly IIdentityStorageService _identityStorageService;
    private readonly IPublisher _publisher;


    public EnterInvoiceCommandHandlers(UnitRepositories repositories, IIdentityStorageService identityStorageService, IPublisher publisher)
    {
        _repositories = repositories;
        _identityStorageService = identityStorageService;
        _publisher = publisher;
    }

    public async Task<long> Handle(EnterInvoiceCommand request, CancellationToken cancellationToken)
    {
        var customer = await _repositories.GetRepository<ICustomerRepository>().GetSingleByCondition(new GetCustomerByPhoneSpecification(request.CustomerPhoneNumber));

        if (customer == null)
        {
            var user = UserBase.CreateInstance(UserExpireDate.Empty);
            user.SetVisitor(new Visitor());

            var userId = await _repositories.GetRepository<IUserRepository>().AddAsync(user);
            customer = Customer.CreateInstance(request.CustomerPhoneNumber, userId);
            customer.Id = await _repositories.GetRepository<ICustomerRepository>().AddAsync(customer);
        }

        var currentUserId = _identityStorageService.GetIdentityItem<UserId>("UserId");

        var shop = await _repositories.GetRepository<IShopRepository>().GetSingleByCondition(new GetShopByUserSpecification(currentUserId));

        var items = request.InvoiceItems.Select(x => x.ToModel()).ToList();

        var invoice = Invoice.Create(customer.Id, shop.Id, request.RawCost, request.DiscountRate, request.DiscountFee, request.FinalCost, items);

        var invoiceId = await _repositories.GetRepository<IInvoicesRepository>().AddAsync(invoice);

        await _repositories.SaveChangeAsync();

        await _publisher.Publish(new InvoiceAddNotification(invoiceId, invoice.FinalCost, customer.Id), cancellationToken);

        return invoiceId.Id;

    }
}