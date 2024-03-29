﻿using System.Runtime.InteropServices;
using MediatR;
using NoPrint.Application.CommandsAndQueries.Customer.Commands;
using NoPrint.Application.CommandsAndQueries.Shop.Commands;
using NoPrint.Application.Ef;
using NoPrint.Application.Ef.Specifications;
using NoPrint.Customers.Domain.Repository;
using NoPrint.Ef;
using NoPrint.Framework.Validation;
using NoPrint.Users.Domain.Repository;
using NoPrint.Users.Domain.ValueObjects;

namespace NoPrint.Application.Services.Handlers;

public class FillCustomerCommandHandlers : IRequestHandler<FillCustomerCommand, long>
{
    private readonly UnitRepositories _repositories;


    public FillCustomerCommandHandlers(UnitRepositories repositories)
    {
        _repositories = repositories;
    }

    public async Task<long> Handle(FillCustomerCommand request, CancellationToken cancellationToken)
    {
        var findUser = !await _repositories.GetRepository<IUserRepository>()
             .AnyExistWithThisUsername(request.UserName);

        findUser.TrueCheck("Username", "Error_UsernameIsUniq");

        var customer = await _repositories.GetRepository<ICustomerRepository>().GetByIdAsync(request.GetCustomerId());
        var user = await _repositories.GetRepository<IUserRepository>().GetByIdAsync(customer.User);

        customer.FillData(request.CustomerName, request.CustomerAddress);
        user.SetUser(User.CreateInstance(request.UserName, request.Password));

        await _repositories.SaveChangeAsync();

        return request.CustomerId;
    }
}