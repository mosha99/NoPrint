namespace NoPrint.Framework.Validation;

public interface IValidationExecutor<T>
{
    public void Execute(T model, List<PropertyError> Errors);
}