using DataAccessLayer;

namespace BusinessLayer
{
    public class UserRepository
    {
        public User? AddUser(string name, string password)
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
    }
}
