namespace NoPrint.Application.Infra;

public interface IAccessManagerService
{
    public void AccessToSend<T>();
    public void AccessToSend(object command);
}