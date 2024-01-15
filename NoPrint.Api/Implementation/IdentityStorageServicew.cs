using NoPrint.Application.Infra;
using NoPrint.Framework.Validation;

namespace NoPrint.Api.Implementation;

public class IdentityStorageService : IIdentityStorageService
{
    private readonly IHttpContextAccessor _httpContext;

    public IdentityStorageService(IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;
    }

    public void SetIdentityItem(string key, object value)
    {
        if (_httpContext.HttpContext.Items.TryAdd(key, value))
        {
            _httpContext.HttpContext.Items[key] = value;
        }
    }

    public T? GetIdentityItem<T>(string key) where T : class
    {
        return GetIdentityItem(key) as T;
    }

    public object? GetIdentityItem(string key)
    {
        return _httpContext?.HttpContext?.Items[key];
    }
}