using MediatR;
using Microsoft.Extensions.Primitives;
using NoPrint.Api.Implementation;
using NoPrint.Application.CommandsAndQueries;
using NoPrint.Application.Infra;
using System.Data;
using System.Reflection;
using System.Security.Authentication;
using System.Text.Json;
using NoPrint.Application.CommandsAndQueries.User;
using NoPrint.Framework;
using NoPrint.Framework.Validation;
using NoPrint.Identity.Share;
using NoPrint.Users.Domain.Repository;
using Rule = NoPrint.Application.Infra.Rule;
using NoPrint.Application.CommandsAndQueries.Interfaces;
using NoPrint.Application.CommandsAndQueries.CommandValidator;
using NoPrint.Framework.Exceptions;

namespace NoPrint.Api.CustomPipe;

public class CommandExecutor
{

    private readonly ISender _sender;
    private readonly IAccessManagerService _accessManagerService;
    private readonly ITokenService _tokenService;
    private readonly IIdentityStorageService _identityStorageService;
    private readonly IServiceProvider _serviceProvider;

    public CommandExecutor(
        ISender sender,
        IAccessManagerService accessManagerService,
        ITokenService tokenService,
        IIdentityStorageService identityStorageService,
        IServiceProvider serviceProvider)
    {
        _sender = sender;
        _accessManagerService = accessManagerService;
        _tokenService = tokenService;
        _identityStorageService = identityStorageService;
        _serviceProvider = serviceProvider;
    }

    public async Task<object?> Execute(string commandName, JsonElement request, HttpRequest httpRequest)
    {
        if (httpRequest.Headers.TryGetValue("NS", out StringValues nameSpace))
        {
            commandName = $"{nameSpace}.{commandName}";
        }

        var command = CreateCommand(commandName, request);

        await Validate(command, _serviceProvider);

        await Authenticate(commandName, httpRequest, command);

        return await _sender.Send(command);

    }
    private async Task Authenticate(string commandName, HttpRequest httpRequest, object command)
    {
        try
        {
            var accessor = typeof(CommandsFlag).Assembly.GetType(commandName)
                .GetCustomAttribute<AccessAttribute>();

            accessor.ValidationCheck(x => x is not null, "Error_AccessDenied");

            if (accessor?.Accessors?.Any(x => x == Rule.NonAuthorize) != true)
            {
                if (httpRequest.Headers.TryGetValue("aut", out StringValues token))
                {
                    var userId = _tokenService.ValidateToken(token, out Rule rule, out Guid key);

                    await _sender.Send(new CheckUserLoginIdQuery(userId, key));

                    _identityStorageService.SetIdentityItem("UserId", userId);

                    _identityStorageService.SetIdentityItem("Rule", rule);

                    _accessManagerService.AccessToSend(command);
                }
                else throw new AuthenticationException();
            }
        }
        catch (Exception e)
        {
            throw new AuthenticationException();
        }

    }
    private async Task Validate(object command, IServiceProvider serviceProvider)
    {
        var interfaces = command
             .GetType()
             .GetInterfaces()
             .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IValidateAble<>));


        foreach (var item in interfaces)
        {
            var validatorType = item.GetGenericArguments()[0];

            var validator = ActivatorUtilities.CreateInstance(serviceProvider, validatorType) as ICommandValidator;

            if (validator is null) throw new Exception();

            await validator.Validate(command);
        }

    }
    private object CreateCommand(string commandName, JsonElement request)
    {
        var commandType = typeof(CommandsFlag).Assembly.GetType(commandName);

        if (commandType == null || !typeof(IBaseRequest).IsAssignableFrom(commandType)) throw new BadRequestException("Error_CommandNotFind");

        if (typeof(IInternalRequest).IsAssignableFrom(commandType) ||
           commandType.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IInternalRequest<>))) throw new BadRequestException("Error_PrivateCommand");

        object? command = request.Deserialize(commandType);

        if (command == null) throw new BadRequestException("Error_DeserializeCommandError");

        return command;
    }
}