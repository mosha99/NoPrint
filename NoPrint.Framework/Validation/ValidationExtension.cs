using NoPrint.Framework.Exceptions;
using System.Xml.Linq;

namespace NoPrint.Framework.Validation;

public static class ValidationExtension
{


    public static void ValidationCheck<T>(this T value, string name, Func<T, bool> validation, params string[] errors)
    {
        if (!validation(value)) throw new InvalidPropertyException(name, errors);
    }
    public static void ValidationCheck<T>(this T value, Func<T, bool> validation, params string[] errors)
    {
        if (!validation(value)) throw new InvalidPropertyException(errors);
    }
    public static void AddValidation<T>(this T value, List<LogicalError> list, string name, Func<T, bool> validation, params string[] errors)
    {
        if (!validation(value)) list.Add(new PropertyError() { Errors = errors?.ToList(), PropTitle = name });

    }
    public static void AddValidation<T>(this T value, List<LogicalError> list, Func<T, bool> validation, params string[] errors)
    {
        if (!validation(value)) list.Add(new LogicalError() { Errors = errors?.ToList() });
    }
    public static void TrueCheck(this bool value, string name, params string[] errors)
    {
        if (!value) throw new InvalidPropertyException(name, errors);
    }

    public static Validator<T> Validate<T>(this T value, Func<T, bool> validation)
    {
        var x =typeof(T);
        return new Validator<T>(value, validation);
    }
    public static AsyncValidator<T> ValidateAsync<T>(this T value, Func<T, Task<bool>> validation)
    {
        return new AsyncValidator<T>(value, validation);
    }

}

public class Validator<T>
{
    protected readonly T _value;
    private readonly Func<T, bool> _validation;

    protected string _name;
    private string[] _errors;

    public Validator(T value, Func<T, bool> validation)
    {
        _value = value;
        _validation = validation;

    }

    protected string[] GetErrors()
    {
        if (_errors is null) return new[] { "Error_PropertyNotValid" };
        return _errors;
    }

    protected virtual bool Validate() => _validation(_value);

    public void ToException()
    {
        if (Validate()) return;
        if (string.IsNullOrWhiteSpace(_name)) throw new InvalidPropertyException(_name, GetErrors());
    }
    public void AddToList(List<LogicalError> errors)
    {
        if (Validate()) return;

        if (string.IsNullOrWhiteSpace(_name)) errors.Add(new LogicalError(GetErrors().ToList()));
        else errors.Add(new PropertyError(_name, GetErrors().ToList()));

    }
    private void SetName(string name) => _name = name;
    private void SetError(string[] errors) => _errors = errors;
    public Validator<T> WithName(string name)
    {
        SetName(name);
        return this;
    }
    public Validator<T> WithError(params string[] errors)
    {
        SetError(errors);
        return this;
    }
}
public class AsyncValidator<T>
{
    private readonly Func<T, Task<bool>> _asyncValidation;

    protected readonly T _value;

    protected string _name;

    private string[] _errors;


    public AsyncValidator(T value, Func<T, Task<bool>> validation)
    {
        _asyncValidation = validation;
        _value = value;
    }

    protected  bool Validate()
    {
        Task<bool> t = _asyncValidation(_value);

        t.Wait();

        return t.Result;
    }

    public async Task ToExceptionAsync()
    {
        if (await _asyncValidation(_value)) return;
        if (string.IsNullOrWhiteSpace(_name)) throw new InvalidPropertyException(_name, GetErrors());
    }
    public async Task AddToListAsync(List<LogicalError> errors)
    {
        if (await _asyncValidation(_value)) return;

        if (string.IsNullOrWhiteSpace(_name)) errors.Add(new LogicalError(GetErrors().ToList()));
        else errors.Add(new PropertyError(_name, GetErrors().ToList()));

    }

    protected string[] GetErrors()
    {
        if (_errors is null) return new[] { "Error_PropertyNotValid" };
        return _errors;
    }
    public void ToException()
    {
        if (Validate()) return;
        if (string.IsNullOrWhiteSpace(_name)) throw new InvalidPropertyException(_name, GetErrors());
    }
    public void AddToList(List<LogicalError> errors)
    {
        if (Validate()) return;

        if (string.IsNullOrWhiteSpace(_name)) errors.Add(new LogicalError(GetErrors().ToList()));
        else errors.Add(new PropertyError(_name, GetErrors().ToList()));

    }

    private void SetName(string name) => _name = name;
    private void SetError(string[] errors) => _errors = errors;
    public AsyncValidator<T> WithName(string name)
    {
        SetName(name);
        return this;
    }
    public AsyncValidator<T> WithError(params string[] errors)
    {
        SetError(errors);
        return this;
    }
}