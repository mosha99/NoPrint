using Microsoft.EntityFrameworkCore;
using NoPrint.Application.Ef.Base;
using NoPrint.Framework.Validation;
using NoPrint.Identity.Share;
using NoPrint.Users.Domain.Models;
using NoPrint.Users.Domain.Repository;

namespace NoPrint.Application.Ef.Repositories;

public class UserRepository : RepositoryBase<UserBase, UserId>, IUserRepository
{
    public UserRepository(NoPrintContext context) : base(context)
    {
    }

    public async Task<Guid> CheckUserLogin(UserId userId, Guid loginId , bool simpleMode = false)
    {
        var user = await _context.Set<UserBase>().SingleOrDefaultAsync(x => x._Id == userId.Id);

        user.ValidationCheck(x => x is not null, "Error_UserNotFind");

        var loginInstance = user.LoginInstances.SingleOrDefault(x => x.LoginId.Equals(loginId));

        loginInstance.ValidationCheck(x => x is not null, "Error_TokenInvalid");

        loginInstance.ValidationCheck(x => !x.ExpireDate.IsExpire(), "Error_TokenInvalid");

        user.ValidationCheck(x => x.CanLogin, "Error_UserCanNotLogin");

        user.ValidationCheck(x => x.ExpireDate?.IsExpire() != true, "Error_UserExpire");

        return loginInstance.Next(simpleMode);

    }

    public async Task<bool> AnyExistWithThisUsername(string username)
    {
        return await _context.Set<UserBase>().AnyAsync(x => x.User.UserName.Equals(username));
    }
}