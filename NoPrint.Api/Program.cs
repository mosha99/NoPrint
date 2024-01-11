
using MediatR;
using NoPrint.Application.CommandsAndQueries.Shop.Commands;
using NoPrint.Application.Services.Handlers;
using NoPrint.Ef;
using NoPrint.Ef.Repositories;
using NoPrint.Shops.Domain.Repository;
using NoPrint.Users.Domain.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(o => o.RegisterServicesFromAssemblies(typeof(CreateShopCommandHandlers).Assembly));
builder.Services.AddDbContext<NoPrintContext>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IShopRepository, ShopRepository>();

builder.Services.AddTransient<UnitRepositories>();


var app = builder.Build();



app.MapGet("/", async (ISender sender) =>
{
    await sender.Send(new CreateShopCommand()
    {
        Password = "111111111111111",
        PhoneNumber = "09013231042",
        ShopAddress = "a11111111111111",
        ShopName = "n11111111111111",
        UserName = "u11111111111111"
    });
});

app.Run();

