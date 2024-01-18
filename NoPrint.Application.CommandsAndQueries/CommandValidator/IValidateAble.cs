using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace NoPrint.Application.CommandsAndQueries.CommandValidator;

/// <summary>
/// Marker
/// </summary>
/// <typeparam name="TValidator"></typeparam>
public interface IValidateAble<out TValidator> where TValidator : class, ICommandValidator
{

}