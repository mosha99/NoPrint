namespace NoPrint.Framework.Validation;

public class ErrorStepOne<T, Y>
{
    public ErrorStepOne(string propName, Action<IValidationExecutor<T>> addToList)
    {
        PropName = propName;
        AddToList = addToList;
    }

    private Action<IValidationExecutor<T>> AddToList;
    private string PropName { get; set; }
    public ErrorStepTwo<T, Y> SetCondition(Func<Y, bool> condition)
    {
        return new ErrorStepTwo<T, Y>(PropName, condition, AddToList);
    }
}