namespace NoPrint.Users.Domain.Models;

public class TokenBehavior
{
    public TokenBehavior(string token, DateTime expireDate)
    {
        Token = token;
        ExpireDate = expireDate;
    }

    public string Token { get; private set; }
    public DateTime ExpireDate { get;private  set; }
}