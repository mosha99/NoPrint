using NoPrint.Identity.Share;

namespace NoPrint.Application.Infra;

public interface ITokenService
{
    public TokenBehavior GenerateToken(UserId userId, Guid loginId, Rule rule);
    public UserId ValidateToken(string token, out Rule rule, out Guid loginId);
}