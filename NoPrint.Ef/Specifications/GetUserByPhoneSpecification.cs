using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using NoPrint.Framework.Specification;
using NoPrint.Framework.Validation;
using NoPrint.Users.Domain.Models;
using System.Numerics;

namespace NoPrint.Ef.Specifications;

public class GetUserByPhoneSpecification : IBaseGetSpecification<UserBase>
{
    private readonly string _phone;

    public GetUserByPhoneSpecification(string phone)
    {
        phone.ValidationCheck("PhoneNumber", x => !string.IsNullOrWhiteSpace(x), "E1034");
        _phone = phone;
    }
    public async Task<UserBase> GetAsync(IQueryable<UserBase> queryable)
    {
        return await queryable.SingleAsync(x => x.Visitor.PhoneNumber == _phone);
    }
}