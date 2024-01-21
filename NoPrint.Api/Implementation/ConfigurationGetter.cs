using Microsoft.Extensions.Configuration;
using NoPrint.Application.Infra;

namespace NoPrint.Api.Implementation;

public class ConfigurationGetter : IConfigurationGetter
{
    private readonly IConfiguration _configuration;
    private IConfigurationSection AuthSection { get; set; }
    public ConfigurationGetter(IConfiguration configuration)
    {
        _configuration = configuration;
        AuthSection = configuration.GetSection("Aut");
    }
    public string Getkey() =>AuthSection["key"];
    public string GetIssuer()=>AuthSection["issuer"];
    public string GetAudience()=>AuthSection["audience"];
    public string GetEnKey()=>AuthSection["enKey"];
    public bool GetIsTokenAdvanceMode()=> _configuration.GetValue<bool>("tokenAdvanceMode");
    public int GetTokenExpireMin() => _configuration.GetValue<int>("tokenExpireMin");
    public string GetUrl() => _configuration.GetValue<string>("appUrl");
}