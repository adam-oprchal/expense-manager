using System.Security.Cryptography;
using System.Text;
using DataAccessLayer;

namespace BusinessLayer
{
    public static class UserRepository
    {
        public static User CreateUser(string name, string password)
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

        public static User DeleteUser(string username)
        {
            using (var db = new ExpenseDbContext())
            {
                try
                {
                    var res = db.Users.Remove(ReadUser(username));
                    db.SaveChanges();
                    return res.Entity;
                } catch 
                {
                    return null;
                }
            }
        }

        public static User ReadUser(string username)
        {
            using ( var db = new ExpenseDbContext())
            {
                try
                {
                    var user = db.Users.Single(u => u.Name == username);
                    return user;
                } catch { return null; }
            }
        }

        public static User ValidateLogin(string username, string password)
        {
            var hashedPassword = Hash(password);

            var user = ReadUser(username);
            if (user == null || user.HashedPassword != hashedPassword)
                return null;

            return user;
        }

        private static string Hash(string input)
        {
            using var hash = SHA256.Create();
            var bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToHexString(bytes);
        }
    }
}
