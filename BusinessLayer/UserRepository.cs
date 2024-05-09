using DataAccessLayer;

namespace BusinessLayer
{
    public static class UserRepository
    {
        public static User AddUser(string name, string password)
        {
            using (var db = new ExpenseDbContext())
            {
                try
                {
                    var res = db.Users.Add(
                        new User { Name = name, HashedPassword = password }
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
            using ( var db = new ExpenseDbContext())
            {
                try
                {
                    var user = db.Users.Single(u => u.Name == username);
                    return password == user.HashedPassword ? user : null;
                } catch { return null; }
            }
        }
    }
}
