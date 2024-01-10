using Microsoft.EntityFrameworkCore;
using Noprint.Identity.Share;
using NoPrint.Ef.Base;
using NoPrint.Users.Domain.Models;
using NoPrint.Users.Domain.Repository;

namespace NoPrint.Ef.Repositories;

public class UserRepository : RepositoryBase<UserBase, UserId>, IUserRepository
{
    public UserRepository(NoPrintContext context) : base(context)
    {
    }
}