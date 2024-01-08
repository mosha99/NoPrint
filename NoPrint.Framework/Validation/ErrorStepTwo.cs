namespace NoPrint.Framework.Validation;

public class ErrorStepTwo<T, Y>
{
    public ErrorStepTwo(string propName, Func<Y, bool> condition, Action<IValidationExecutor<T>> addToList)
    {
        PropName = propName;
        Condition = condition;
        AddToList = addToList;
    }

    private Action<IValidationExecutor<T>> AddToList;
    private string PropName { get; set; }
    private Func<Y, bool> Condition { get; set; }

    public void WithError(string error)
    {
        AddToList(new ValidationExecutor<T, Y>(PropName, Condition, error));

    }
}