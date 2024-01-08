using NoPrint.Users.Domain.Models;

namespace NoPrint.Users.Domain.Services;

public interface IJwtService
{
    public Task<TokenBehavior> CreateToken(Dictionary<string,object> claims);
    public Task<Dictionary<string, object>> ValidationTokenAndGetClaims(string token);
}