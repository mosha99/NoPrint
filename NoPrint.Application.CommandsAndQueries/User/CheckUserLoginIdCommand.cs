using MediatR;
using NoPrint.Identity.Share;

namespace NoPrint.Application.CommandsAndQueries.User;

public class CheckUserLoginIdCommand: IRequest
{
    public CheckUserLoginIdCommand(UserId userId,Guid loginId) 
    {
        LoginId = loginId;
        UserId = userId;
    }

    public UserId UserId { get; set; }  
    public Guid LoginId { get; set; }
}