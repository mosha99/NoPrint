using MediatR;
using NoPrint.Application.CommandsAndQueries.Customer.Commands;
using NoPrint.Application.Ef;
using NoPrint.Application.Ef.Specifications;
using NoPrint.Application.Infra;
using NoPrint.Customers.Domain.Repository;
using NoPrint.Ef;
using NoPrint.Framework.Validation;
using NoPrint.Users.Domain.Repository;
using NoPrint.Users.Domain.Tools;

namespace NoPrint.Application.Services.Handlers;

public class GetTokenForCustomerByUserNameCommandHandlers : IRequestHandler<GetTokenForCustomerByUserNameCommand, TokenBehavior>
{
    private readonly UnitRepositories _repositories;
    private readonly ITokenService _tokenService;
    private readonly IIdentityStorageService _identityStorageService;
    private readonly IConfigurationGetter _configurationGetter;


    public GetTokenForCustomerByUserNameCommandHandlers(
        UnitRepositories repositories,
        ITokenService tokenService,
        IIdentityStorageService identityStorageService , IConfigurationGetter configurationGetter)
    {
        _repositories = repositories;
        _tokenService = tokenService;
        _identityStorageService = identityStorageService;
        _configurationGetter = configurationGetter;
    }

    public async Task<TokenBehavior> Handle(GetTokenForCustomerByUserNameCommand request, CancellationToken cancellationToken)
    {
        var user = await _repositories.GetRepository<IUserRepository>().GetSingleByCondition(new GetUserByUserNameAndPasswordSpecification(request.UserName, request.Password));

        user.ValidationCheck("UserName", x => x is not null, "Error_NotFind");

        var customer = await _repositories.GetRepository<ICustomerRepository>().GetSingleByCondition(new GetCustomerByUserSpecification(user.Id));

        customer.ValidationCheck("Customer",x => x is not null, "Error_NotFind");

        var deviceInfo = _identityStorageService.GetIdentityItem<string>("User-Agent");

        var em = _configurationGetter.GetTokenExpireMin();

        var loginId = user.LoginByUserName(deviceInfo,em);

        await _repositories.SaveChangeAsync();

        return _tokenService.GenerateToken(user.Id, loginId, Rule.Customer_User);
    }
}