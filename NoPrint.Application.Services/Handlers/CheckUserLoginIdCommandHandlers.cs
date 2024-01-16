using MediatR;
using NoPrint.Application.CommandsAndQueries.User;
using NoPrint.Users.Domain.Repository;

namespace NoPrint.Application.Services.Handlers;

public class CheckUserLoginIdCommandHandlers : IRequestHandler<CheckUserLoginIdCommand>
{
    private readonly IUserRepository _userRepository;


    public CheckUserLoginIdCommandHandlers(IUserRepository userRepository )
    {
        _userRepository = userRepository;
    }

    public async Task Handle(CheckUserLoginIdCommand request, CancellationToken cancellationToken)
    {
        await _userRepository.CheckUserLogin(request.UserId, request.LoginId);
    }
}