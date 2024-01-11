using Microsoft.EntityFrameworkCore;
using NoPrint.Ef.Base;
using NoPrint.Identity.Share;
using NoPrint.Users.Domain.Models;
using NoPrint.Users.Domain.Repository;

namespace NoPrint.Ef.Repositories;

public class UserRepository : RepositoryBase<UserBase, UserId>, IUserRepository
{
    public UserRepository(NoPrintContext context) : base(context)
    {
    }
}