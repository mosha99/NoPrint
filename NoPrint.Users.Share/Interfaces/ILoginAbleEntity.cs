namespace NoPrint.Users.Share.Interfaces;

public interface ILoginAbleEntity
{
    public long UserId { get; }
    public bool CanLogin{ get; }
    public void Disable();
    public void Enable();
}

public interface ILoginAbleEntityByPassword : ILoginAbleEntity
{
    public string UserName { get; }
    public string Password { get; }
    public string PhoneNumber { get; }
}
public interface ILoginAbleEntityByCode : ILoginAbleEntity
{
    public string PhoneNumber { get; }
    public string Code { get; }
}
