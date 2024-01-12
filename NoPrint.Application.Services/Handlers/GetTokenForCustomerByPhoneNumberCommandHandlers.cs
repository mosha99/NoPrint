using MediatR;
using NoPrint.Application.CommandsAndQueries.Customer.Commands;
using NoPrint.Customers.Domain.Repository;
using NoPrint.Ef;
using NoPrint.Ef.Specifications;
using NoPrint.Framework.Validation;
using NoPrint.Users.Domain.Repository;

namespace NoPrint.Application.Services.Handlers;

public class GetTokenForCustomerByPhoneNumberCommandHandlers : IRequestHandler<GetTokenForCustomerByPhoneNumberCommand, string>
{
    private readonly UnitRepositories _repositories;


    public GetTokenForCustomerByPhoneNumberCommandHandlers(UnitRepositories repositories)
    {
        _repositories = repositories;
    }

    public async Task<string> Handle(GetTokenForCustomerByPhoneNumberCommand request, CancellationToken cancellationToken)
    {
        var customer = await _repositories.GetRepository<ICustomerRepository>().GetSingleByCondition(new GetCustomerByPhoneSpecification(request.PhoneNumber));

        customer.ValidationCheck("PhoneNumber", x => x is not null, "E1035");

        var user = await _repositories.GetRepository<IUserRepository>().GetByIdAsync(customer.User);

        user.LoginByPhone(customer, request.Code);

        await _repositories.SaveChangeAsync();

        return "";
    }
}