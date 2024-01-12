using MediatR;
using NoPrint.Application.CommandsAndQueries.Customer.Commands;
using NoPrint.Customers.Domain.Repository;
using NoPrint.Ef;
using NoPrint.Ef.Specifications;
using NoPrint.Framework.Validation;
using NoPrint.Users.Domain.Repository;

namespace NoPrint.Application.Services.Handlers;

public class GetTokenForCustomerByUserNameCommandHandlers : IRequestHandler<GetTokenForCustomerByUserNameCommand, string>
{
    private readonly UnitRepositories _repositories;


    public GetTokenForCustomerByUserNameCommandHandlers(UnitRepositories repositories)
    {
        _repositories = repositories;
    }

    public async Task<string> Handle(GetTokenForCustomerByUserNameCommand request, CancellationToken cancellationToken)
    {
        var user = await _repositories.GetRepository<IUserRepository>().GetSingleByCondition(new GetUserByUserNameAndPasswordSpecification(request.UserName, request.Password));

        user.ValidationCheck("UserName", x => x is not null, "E1035");

        var customer = await _repositories.GetRepository<ICustomerRepository>().GetSingleByCondition(new GetCustomerByUserSpecification(user.Id));

        customer.ValidationCheck(x => x is not null, "E1035");

        user.LoginByUserName();

        await _repositories.SaveChangeAsync();

        return "";
    }
}