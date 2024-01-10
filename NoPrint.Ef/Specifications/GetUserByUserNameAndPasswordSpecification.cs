using Microsoft.EntityFrameworkCore;
using NoPrint.Framework.Specification;
using NoPrint.Framework.Validation;
using NoPrint.Users.Domain.Models;
using NoPrint.Users.Domain.ValueObjects;

namespace NoPrint.Ef.Specifications;

public class GetUserByUserNameAndPasswordSpecification : IBaseGetSpecification<UserBase>
{
    private readonly string _userName;
    private readonly string _password;

    public GetUserByUserNameAndPasswordSpecification(string userName, string password)
    {
        userName.ValidationCheck("UserName", x => !string.IsNullOrWhiteSpace(x), "E1034");
        password.ValidationCheck("Password", x => !string.IsNullOrWhiteSpace(x), "E1035");

        _userName = userName;
        _password = password;
    }
    public async Task<UserBase> GetAsync(IQueryable<UserBase> queryable)
    {
        return await queryable.SingleAsync(x => x.User.Password == _password && x.User.UserName == _userName);
    }
}