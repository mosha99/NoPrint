using NoPrint.Users.Domain.Models;
using NoPrint.Users.Share.Interfaces;

namespace NoPrint.Users.Domain.Repository;

public interface IUserRepository
{
    Task<ILoginAbleEntity> GetUser(long Id);
    Task<ILoginAbleEntityByPassword> GetUserByUserName(string userName , string password);
    Task<ILoginAbleEntityByCode> GetUserByPhoneNumber(string phoneNumber);

    Task<TokenBehavior> Login(ILoginAbleEntityByPassword entity);
    Task<TokenBehavior> Login(ILoginAbleEntityByCode entity ,string code);

    public Task SendCodeToNumber(ILoginAbleEntity entity);
    public Task CheckIsLogin(string token);
}

