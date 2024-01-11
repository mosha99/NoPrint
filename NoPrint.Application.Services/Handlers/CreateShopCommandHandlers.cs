using MediatR;
using NoPrint.Application.CommandsAndQueries.Shop.Commands;
using NoPrint.Ef.Repositories;
using NoPrint.Identity.Share;
using NoPrint.Shops.Domain.Models;
using NoPrint.Shops.Domain.Repository;
using NoPrint.Users.Domain.Models;
using NoPrint.Users.Domain.Repository;
using NoPrint.Users.Domain.ValueObjects;

namespace NoPrint.Application.Services.Handlers;

public class CreateShopCommandHandlers : IRequestHandler<CreateShopCommand, long>
{
    private readonly IShopRepository _shopRepository;
    private readonly IUserRepository _userRepository;

    public CreateShopCommandHandlers(IShopRepository shopRepository, IUserRepository userRepository)
    {
        _shopRepository = shopRepository;
        _userRepository = userRepository;
    }

    public async Task<long> Handle(CreateShopCommand request, CancellationToken cancellationToken)
    {

        var user = UserBase.CreateInstance(new UserExpireDate(null));

        user.SetUser(User.CreateInstance(request.UserName, request.Password));

        UserId userId = await _userRepository.AddAsync(user);

        var shop = Shop.CreateInstance(request.ShopName, request.PhoneNumber, request.ShopAddress, userId);

        ShopId shopId = await _shopRepository.AddAsync(shop);

        await _shopRepository.Save();

        return shopId.Id;
    }
}