using System.Data;
using NoPrint.Framework;
using NoPrint.Identity.Share;
using NoPrint.Users.Domain.Models;

namespace NoPrint.Users.Domain.Repository;

public interface IUserRepository : IRepositoryBase<UserBase, UserId>
{
    public Task CheckUserLogin(UserId userId, Guid loginId);
    public Task<bool> AnyExistWithThisUsername(string username);
}