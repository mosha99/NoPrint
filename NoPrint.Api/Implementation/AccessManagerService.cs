using System.Reflection;
using NoPrint.Application.Infra;
using NoPrint.Framework;
using NoPrint.Users.Domain.Tools;

namespace NoPrint.Api.Implementation;

public class AccessManagerService : IAccessManagerService
{
    private readonly IIdentityStorageService _identityStorageService;

    public AccessManagerService(IIdentityStorageService identityStorageService)
    {
        _identityStorageService = identityStorageService;
    }

    public void AccessTo(Type? type)
    {
        var access = type?.GetCustomAttribute<AccessAttribute>()?.Accessors;

        var userKind = _identityStorageService.GetIdentityItem("Rule");

        if(access?.Any(x=>x.Equals(Rule.NonAuthorize)) == true) return;

        if (access?.Any(y => y.Equals(userKind)) != true)
        {
            throw new UnauthorizedAccessException();
        }
    }
    public void AccessToSend<T>()
    {
        AccessTo(typeof(T));
    }

    public void AccessToSend(object command)
    {
        AccessTo(command.GetType());
    }
}