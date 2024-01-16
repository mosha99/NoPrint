using MediatR;

namespace NoPrint.Application.CommandsAndQueries.Interfaces;

public interface IInternalRequest<T> : IRequest<T> { }

public interface IInternalRequest : IRequest
{
}