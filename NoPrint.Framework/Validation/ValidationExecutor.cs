namespace NoPrint.Framework.Validation;

public class ValidationExecutor<T, Y> : IValidationExecutor<T>
{
    public ValidationExecutor(string propName, Func<Y, bool> condition, string error)
    {
        PropName = propName;
        Condition = condition;
        Error = error;
    }

    private string PropName { get; set; }
    private Func<Y, bool> Condition { get; set; }
    private string Error { get; set; }
    public void Execute(T model, List<PropertyError> Errors)
    {
        var prop = typeof(T).GetProperties().Single(x => x.Name == PropName);
        var value = prop.GetValue(model);

        bool isValidate = Condition((Y)value);
        if (!isValidate)
        {
            if (Errors.Any(x => x.PropTitle == PropName))
            {
                Errors.Single(x => x.PropTitle == PropName).Errors.Add(Error);
            }
            else
            {
                Errors.Add(new PropertyError() { PropTitle = PropName, Errors = new List<string>() { Error } });
            }
        }
    }
}