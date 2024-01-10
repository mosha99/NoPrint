using Microsoft.EntityFrameworkCore;
using Noprint.Identity.Share;
using NoPrint.Users.Domain.Models;
using NoPrint.Users.Domain.Repository;

namespace NoPrint.Ef.Repositories;

public class UserRepository : RepositoryBase<UserBase, UserId>, IUserRepository
{
    public UserRepository(DbContext context) : base(context)
    {
    }
}