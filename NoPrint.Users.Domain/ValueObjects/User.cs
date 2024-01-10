using System.Security.Cryptography.X509Certificates;
using NoPrint.Framework.Identity;

namespace NoPrint.Users.Domain.ValueObjects
{
    public class User
    {
        private User() { }

        public static User CreateInstance(string userName, string password)
        {
            return new User()
            {
                UserName = userName,
                Password = password
            };
        }


        public string UserName { get; private set; }
        public string Password { get; private set; }

        public override bool Equals(object? obj) => obj is User user && user.Password.Equals(Password) && user.UserName.Equals(UserName);
        public override int GetHashCode() => UserName.GetHashCode() + Password.GetHashCode() * 2;
    }
}
