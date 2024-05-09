using System.Security.Cryptography;
using System.Text;
using DataAccessLayer;

namespace BusinessLayer
{
    public static class UserRepository
    {
        public static User AddUser(string name, string password)
        {
            var hashedPassword = Hash(password);

            using (var db = new ExpenseDbContext())
            {
                try
                {
                    var res = db.Users.Add(
                        new User { Name = name, HashedPassword = hashedPassword }
                    );
                    db.SaveChanges();
                    return res.Entity;

                } catch 
                {
                    return null;
                }
            }
        }

        public static User ValidateLogin(string username, string password)
        {
            var hashedPassword = Hash(password);

            using ( var db = new ExpenseDbContext())
            {
                try
                {
                    var user = db.Users.Single(u => u.Name == username);
                    return hashedPassword == user.HashedPassword ? user : null;
                } catch { return null; }
            }
        }

        private static string Hash(string input)
        {
            using var hash = SHA256.Create();
            var bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToHexString(bytes);
        }
    }
}
