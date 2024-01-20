using MediatR;
using NoPrint.Application.CommandsAndQueries.Interfaces;
using NoPrint.Identity.Share;

namespace NoPrint.Application.CommandsAndQueries.User;

public class CheckUserLoginIdQuery: IInternalRequest
{
    public CheckUserLoginIdQuery(UserId userId,Guid loginId) 
    {
        LoginId = loginId;
        UserId = userId;
    }

    public UserId UserId { get; set; }  
    public Guid LoginId { get; set; }
}