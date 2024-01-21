using MediatR;
using NoPrint.Application.CommandsAndQueries.User;
using NoPrint.Application.Infra;
using NoPrint.Invoices.Domain.Models;
using NoPrint.Users.Domain.Repository;

namespace NoPrint.Application.Services.Handlers;

public class CheckUserLoginIdCommandHandlers : IRequestHandler<CheckUserLoginIdQuery, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IConfigurationGetter _configurationGetter;


    public CheckUserLoginIdCommandHandlers(IUserRepository userRepository , IConfigurationGetter configurationGetter)
    {
        _userRepository = userRepository;
        _configurationGetter = configurationGetter;
    }

    public async Task<Guid> Handle(CheckUserLoginIdQuery request, CancellationToken cancellationToken)
    {
        var simpleMode = !_configurationGetter.GetIsTokenAdvanceMode();
        var id = await _userRepository.CheckUserLogin(request.UserId, request.LoginId ,simpleMode );
        await _userRepository.Save();
        return id;
    }
}