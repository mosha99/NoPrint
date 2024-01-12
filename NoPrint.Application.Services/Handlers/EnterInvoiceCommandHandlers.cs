using MediatR;
using NoPrint.Application.CommandsAndQueries.Invoices.Commands;
using NoPrint.Customers.Domain.Model;
using NoPrint.Customers.Domain.Repository;
using NoPrint.Ef;
using NoPrint.Ef.Specifications;
using NoPrint.Invoices.Domain.Models;
using NoPrint.Invoices.Domain.Repository;
using NoPrint.Users.Domain.Models;
using NoPrint.Users.Domain.Repository;
using NoPrint.Users.Domain.ValueObjects;

namespace NoPrint.Application.Services.Handlers;

public class EnterInvoiceCommandHandlers : IRequestHandler<EnterInvoiceCommand, long>
{
    private readonly UnitRepositories _repositories;


    public EnterInvoiceCommandHandlers(UnitRepositories repositories)
    {
        _repositories = repositories;
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


        var invoice = Invoice.Create(customer.Id, request.GetShopId(), request.RawCost, request.DiscountRate, request.DiscountFee, request.FinalCost, request.InvoiceItems);

        var invoiceId = await _repositories.GetRepository<IInvoicesRepository>().AddAsync(invoice);

        await _repositories.SaveChangeAsync();

        return invoiceId.Id;

    }
}