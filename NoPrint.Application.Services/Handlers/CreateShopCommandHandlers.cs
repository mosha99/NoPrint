using MediatR;
using NoPrint.Application.CommandsAndQueries.Shop.Commands;
using NoPrint.Application.Ef;
using NoPrint.Ef;
using NoPrint.Framework.Validation;
using NoPrint.Identity.Share;
using NoPrint.Shops.Domain.Models;
using NoPrint.Shops.Domain.Repository;
using NoPrint.Users.Domain.Models;
using NoPrint.Users.Domain.Repository;
using NoPrint.Users.Domain.ValueObjects;

namespace NoPrint.Application.Services.Handlers;

public class CreateShopCommandHandlers : IRequestHandler<CreateShopCommand, long>
{
    private readonly UnitRepositories _repositories;


    public CreateShopCommandHandlers(UnitRepositories repositories)
    {
        _repositories = repositories;
    }

    public async Task<long> Handle(CreateShopCommand request, CancellationToken cancellationToken)
    {

        var findUser = !await _repositories.GetRepository<IUserRepository>()
            .AnyExistWithThisUsername(request.UserName);

        findUser.TrueCheck("Username", "Error_UsernameIsUniq");

        var user = UserBase.CreateInstance(new UserExpireDate(null));

        user.SetUser(User.CreateInstance(request.UserName, request.Password));

        UserId userId = await _repositories.GetRepository<IUserRepository>().AddAsync(user);

        var shop = Shop.CreateInstance(request.ShopName, request.PhoneNumber, request.ShopAddress, userId);

        ShopId shopId = await _repositories.GetRepository<IShopRepository>().AddAsync(shop);

        await _repositories.SaveChangeAsync();

        return shopId.Id;
    }
}