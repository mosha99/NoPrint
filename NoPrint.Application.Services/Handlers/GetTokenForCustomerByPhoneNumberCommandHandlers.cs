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

public class GetTokenForCustomerByPhoneNumberCommandHandlers : IRequestHandler<GetTokenForCustomerByPhoneNumberCommand, TokenBehavior>
{
    private readonly UnitRepositories _repositories;
    private readonly ITokenService _tokenService;
    private readonly IIdentityStorageService _identityStorageService;
    private readonly IConfigurationGetter _configurationGetter;


    public GetTokenForCustomerByPhoneNumberCommandHandlers(
        UnitRepositories repositories,
        ITokenService tokenService,
        IIdentityStorageService identityStorageService ,
        IConfigurationGetter configurationGetter)
    {
        _repositories = repositories;
        _tokenService = tokenService;
        _identityStorageService = identityStorageService;
        _configurationGetter = configurationGetter;
    }

    public async Task<TokenBehavior> Handle(GetTokenForCustomerByPhoneNumberCommand request, CancellationToken cancellationToken)
    {
        var customer = await _repositories.GetRepository<ICustomerRepository>()
            .GetSingleByCondition(new GetCustomerByPhoneSpecification(request.PhoneNumber));

        customer.ValidationCheck("PhoneNumber", x => x is not null, "Error_NotFind");

        var user = await _repositories.GetRepository<IUserRepository>().GetByIdAsync(customer.User, true);

        var deviceInfo = _identityStorageService.GetIdentityItem<string>("User-Agent");

        var em = _configurationGetter.GetTokenExpireMin();

        var loginId = user.LoginByPhone(customer, request.Code, deviceInfo,em);

        await _repositories.SaveChangeAsync();

        return _tokenService.GenerateToken(user.Id, loginId, Rule.Customer_Visitor);
    }
}