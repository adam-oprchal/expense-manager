using DataAccessLayer;

namespace BusinessLayer
{
    public static class CategoryRepository
    {
        public static List<Category> ReadAllCategories(string username)
        {
            using (var db = new ExpenseDbContext())
            {
                try
                {
                    return db.Categories
                        .Where(c => c.Username == username)
                        .ToList();
                } catch
                {
                    return null;
                }
            }
        }

        public static Category CreateCategory(string name, string username)
        {
            using (var db = new ExpenseDbContext())
            {
                try
                {
                    var res = db.Categories.Add(
                        new Category 
                        { Name = name, Username = username });
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
