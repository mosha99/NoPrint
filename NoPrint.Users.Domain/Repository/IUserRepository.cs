using System.Data;
using NoPrint.Framework;
using NoPrint.Identity.Share;
using NoPrint.Invoices.Domain.Models;
using NoPrint.Users.Domain.Models;

namespace NoPrint.Users.Domain.Repository;

public interface IUserRepository : IRepositoryBase<UserBase, UserId>
{
    public Task<Guid> CheckUserLogin(UserId userId, Guid loginId, bool simpleMode = false);
    public Task<bool> AnyExistWithThisUsername(string username);
}
