namespace NoPrint.Application.Infra;

public class TokenBehavior
{
    public TokenBehavior(string token, DateTime expireTime)
    {
        Token = token;
        ExpireTime = expireTime;
    }

    public string Token { get; set; }
    public DateTime ExpireTime { get; set; }
}