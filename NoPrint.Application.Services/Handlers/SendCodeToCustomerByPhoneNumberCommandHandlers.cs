using MediatR;
using NoPrint.Application.CommandsAndQueries.Customer.Commands;
using NoPrint.Application.Ef;
using NoPrint.Application.Ef.Specifications;
using NoPrint.Customers.Domain.Repository;
using NoPrint.Ef;
using NoPrint.Framework.Validation;
using NoPrint.Notification.Share;
using NoPrint.Users.Domain.Repository;

namespace NoPrint.Application.Services.Handlers;

public class SendCodeToCustomerByPhoneNumberCommandHandlers : IRequestHandler<SendCodeToCustomerByPhoneNumberCommand>
{
    private readonly UnitRepositories _repositories;
    private readonly IMessageSenderService _senderService;


    public SendCodeToCustomerByPhoneNumberCommandHandlers(UnitRepositories repositories, IMessageSenderService senderService)
    {
        _repositories = repositories;
        _senderService = senderService;
    }

    public async Task Handle(SendCodeToCustomerByPhoneNumberCommand request, CancellationToken cancellationToken)
    {
        var customer = await _repositories.GetRepository<ICustomerRepository>().GetSingleByCondition(new GetCustomerByPhoneSpecification(request.PhoneNumber));

        customer.ValidationCheck("PhoneNumber", x => x is not null, "Error_Required");

        var user = await _repositories.GetRepository<IUserRepository>().GetByIdAsync(customer.User);

        user.SendCode(customer, _senderService);
    }
}