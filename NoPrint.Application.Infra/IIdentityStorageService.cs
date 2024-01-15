namespace NoPrint.Application.Infra;

public interface IIdentityStorageService
{
    public void SetIdentityItem(string key , object value);
    public T GetIdentityItem<T>(string key) where T : class;
    public object? GetIdentityItem(string key);
}