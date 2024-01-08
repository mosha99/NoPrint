using System.Linq.Expressions;

namespace NoPrint.Framework.Validation;

public abstract class CustomValidationBase<T>
{
    protected CustomValidationBase()
    {
        Errors = new List<PropertyError>();
        ValidationExecutors = new List<IValidationExecutor<T>>();
    }
    private List<PropertyError> Errors { get; set; }
    private List<IValidationExecutor<T>> ValidationExecutors { get; set; }
    protected ErrorStepOne<T, Y> RuleFor<Y>(Expression<Func<T, Y>> selector)
    {
        string name = "";

        if (selector.Body is UnaryExpression unaryExpression)
        {
            dynamic d = unaryExpression.Operand;
            name = d.Member.Name;
        }

        if (selector.Body is MemberExpression memberExpression)
        {
            name = memberExpression.Member.Name;
        }

        return new ErrorStepOne<T, Y>(name, AddToList);
    }
    public List<PropertyError> Validate(T model)
    {
        Errors = new List<PropertyError>();
        foreach (var exicuter in ValidationExecutors)
        {
            exicuter.Execute(model, Errors);
        }

        return Errors;
    }
    private void AddToList(IValidationExecutor<T> executor)
    {
        ValidationExecutors.Add(executor);
    }
}