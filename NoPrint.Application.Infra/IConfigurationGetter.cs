namespace NoPrint.Application.Infra;

public interface IConfigurationGetter
{
    public string Getkey();
    public string GetIssuer();
    public string GetAudience();
    public string GetEnKey();
    public bool GetIsTokenAdvanceMode();
    public int GetTokenExpireMin();

}