using MediatR;
using NoPrint.Framework.Validation;

namespace NoPrint.Application.CommandsAndQueries.CommandValidator;
public interface ICommandValidator
{
    public List<LogicalError> Errors { get; set; }
    public Task Validate(object obg);
}
public interface ICommandValidator<in TCommand> : ICommandValidator
{
    public List<LogicalError> Errors { get; set; }

    async Task ICommandValidator.Validate(object obg)
    {
        if (obg is TCommand command)
        {
            await Validate(command);
        }
        else
        {
            throw new Exception("ValidationFail");
        }
    }

    public Task Validate(TCommand command);
}