using MediatR;
using NoPrint.Application.CommandsAndQueries.Shop.Commands;
using NoPrint.Application.Ef;
using NoPrint.Application.Ef.Specifications;
using NoPrint.Application.Infra;
using NoPrint.Ef;
using NoPrint.Framework.Validation;
using NoPrint.Shops.Domain.Repository;
using NoPrint.Users.Domain.Repository;
using NoPrint.Users.Domain.Tools;

namespace NoPrint.Application.Services.Handlers;

public class GetTokenForShopByUserNameCommandHandlers : IRequestHandler<GetTokenForShopByUserNameCommand, TokenBehavior>
{
    private readonly UnitRepositories _repositories;
    private readonly ITokenService _tokenService;


    public GetTokenForShopByUserNameCommandHandlers(UnitRepositories repositories, ITokenService tokenService)
    {
        _repositories = repositories;
        _tokenService = tokenService;
    }

    public async Task<TokenBehavior> Handle(GetTokenForShopByUserNameCommand request, CancellationToken cancellationToken)
    {
        var user = await _repositories.GetRepository<IUserRepository>().GetSingleByCondition(new GetUserByUserNameAndPasswordSpecification(request.UserName, request.Password));

        user.ValidationCheck("UserName", x => x is not null, "E1035");

        var shop = await _repositories.GetRepository<IShopRepository>().GetSingleByCondition(new GetShopByUserSpecification(user.Id));

        shop.ValidationCheck(x => x is not null, "E1035");

        var loginId = user.LoginByUserName();

        await _repositories.SaveChangeAsync();

        return _tokenService.GenerateToken(user.Id, loginId, Rule.Shop_User);
    }
}