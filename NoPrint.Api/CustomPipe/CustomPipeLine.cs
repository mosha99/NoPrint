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

namespace NoPrint.Api.CustomPipe;

public class CustomPipeline
{

    private readonly ISender _sender;
    private readonly IAccessManagerService _accessManagerService;
    private readonly ITokenService _tokenService;
    private readonly IIdentityStorageService _identityStorageService;

    public CustomPipeline(
        ISender sender,
        IAccessManagerService accessManagerService,
        ITokenService tokenService,
        IIdentityStorageService identityStorageService)
    {
        _sender = sender;
        _accessManagerService = accessManagerService;
        _tokenService = tokenService;
        _identityStorageService = identityStorageService;
    }

    public async Task<object?> Execute(string commandName, JsonElement request, HttpRequest httpRequest)
    {
        var command = CreateCommand(commandName, request);

        var accessor = typeof(CommandsFlag).Assembly.GetType(commandName)
            .GetCustomAttribute<AccessAttribute>();

        accessor.ValidationCheck(x => x is not null, "E1035");

        if (accessor?.Accessors?.Any(x => x == Rule.NonAuthorize) != true)
        {
            if (httpRequest.Headers.TryGetValue("aut", out StringValues token))
            {
                var userId = _tokenService.ValidateToken(token, out Rule rule, out Guid key);

                await _sender.Send(new CheckUserLoginIdCommand(userId, key));

                _identityStorageService.SetIdentityItem("UserId", userId);

                _identityStorageService.SetIdentityItem("Rule", rule);

                _accessManagerService.AccessToSend(command);

            }
            else throw new AuthenticationException();
        }

        return await _sender.Send(command);

    }


    private object CreateCommand(string commandName, JsonElement request)
    {
        var commandType = typeof(CommandsFlag).Assembly.GetType(commandName);

        if (commandType == null || !typeof(IBaseRequest).IsAssignableFrom(commandType)) throw new BadRequestException("E1035");

        object? command = request.Deserialize(commandType);

        if (command == null) throw new BadRequestException("E1035");

        return command;
    }
}