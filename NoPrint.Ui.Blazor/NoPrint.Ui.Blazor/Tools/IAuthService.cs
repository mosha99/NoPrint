using NoPrint.Application.Infra;

namespace NoPrint.Ui.Blazor.Tools;

public interface IAuthService
{
    public Task<Rule?> GetLoginRule();
    public Task LoginFactory(string username, string password);
    public Task LoginCustomerByUserName(string username, string password);
    public Task LoginCustomerByPhoneNumber(string phoneNumber, string code);

}
public class AuthService :IAuthService
{
    public async Task<Rule?> GetLoginRule() => Rule.NonAuthorize;

    public Task LoginFactory(string username, string password)
    {
        throw new NotImplementedException();
    }

    public Task LoginCustomerByUserName(string username, string password)
    {
        throw new NotImplementedException();
    }

    public Task LoginCustomerByPhoneNumber(string phoneNumber, string code)
    {
        throw new NotImplementedException();
    }
}