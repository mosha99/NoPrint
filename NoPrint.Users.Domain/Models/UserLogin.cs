namespace NoPrint.Users.Domain.Models;

public class UserLogin
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public bool Success { get; set; }
    public DateTime LoginTime { get; set; }
}