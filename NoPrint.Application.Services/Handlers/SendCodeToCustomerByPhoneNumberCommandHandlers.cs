﻿using MediatR;
using NoPrint.Application.CommandsAndQueries.Customer.Commands;
using NoPrint.Customers.Domain.Repository;
using NoPrint.Ef;
using NoPrint.Ef.Specifications;
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

        customer.ValidationCheck("PhoneNumber", x => x is not null, "E1035");

        var user = await _repositories.GetRepository<IUserRepository>().GetByIdAsync(customer.User);

        user.SendCode(customer, _senderService);
    }
}