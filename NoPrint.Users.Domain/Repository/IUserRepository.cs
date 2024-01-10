using Noprint.Identity.Share;
using NoPrint.Framework;
using NoPrint.Users.Domain.Models;

namespace NoPrint.Users.Domain.Repository;

public interface IUserRepository : IRepositoryBase<UserBase, UserId>
{

}