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

    public async Task CheckUserLogin(UserId userId, Guid loginId)
    {
        var user = await _context.Set<UserBase>().SingleOrDefaultAsync(x => x._Id == userId.Id);

        user.ValidationCheck(x => x is not null, "Error_UserNotFind");

        user.ValidationCheck(x => x.LoginId.Equals(loginId), "Error_TokenInvalid");

        user.ValidationCheck(x=>x.CanLogin, "Error_UserCanNotLogin");

        user.ValidationCheck(x=>x.ExpireDate?.IsExpire() != true, "Error_UserExpire");
    }

}