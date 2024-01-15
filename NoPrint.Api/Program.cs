
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using NoPrint.Api.CustomPipe;
using NoPrint.Application.CommandsAndQueries.Shop.Commands;
using NoPrint.Application.Ef;
using NoPrint.Application.Ef.Repositories;
using NoPrint.Application.Infra;
using NoPrint.Application.Services.Handlers;
using NoPrint.Ef;
using NoPrint.Shops.Domain.Repository;
using NoPrint.Users.Domain.Repository;
using System.Text.Json;
using NoPrint.Api.Implementation;
using NoPrint.Customers.Domain.Repository;
using NoPrint.Notification.Share;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(o => o.RegisterServicesFromAssemblies(typeof(CreateShopCommandHandlers).Assembly));
builder.Services.AddDbContext<NoPrintContext>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IShopRepository, ShopRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();


builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAccessManagerService, AccessManagerService>();
builder.Services.AddScoped<IIdentityStorageService, IdentityStorageService>();
builder.Services.AddScoped<IMessageSenderService, MessageSenderService>();

builder.Services.AddTransient<UnitRepositories>();
builder.Services.AddTransient<CustomPipeline>();

var app = builder.Build();



app.MapPost("Commands/{commandName}",
    async (string commandName, JsonElement request, HttpRequest httpRequest, CustomPipeline customPipeline) =>
    {
        try
        {
            return await customPipeline.Execute(commandName, request, httpRequest);
        }
        catch (BadRequestException e)
        {
            return Results.BadRequest(e.Message);
        }
    }
);

app.Run();

