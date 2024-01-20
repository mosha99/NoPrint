using MediatR;
using NoPrint.Application.CommandsAndQueries.User;
using NoPrint.Invoices.Domain.Models;
using NoPrint.Users.Domain.Repository;

namespace NoPrint.Application.Services.Handlers;

public class CheckUserLoginIdCommandHandlers : IRequestHandler<CheckUserLoginIdQuery>
{
    private readonly IUserRepository _userRepository;


    public CheckUserLoginIdCommandHandlers(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(CheckUserLoginIdQuery request, CancellationToken cancellationToken)
    {
        await _userRepository.CheckUserLogin(request.UserId, request.LoginId);
    }
}