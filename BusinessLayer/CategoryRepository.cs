using DataAccessLayer;
using Microsoft.EntityFrameworkCore;

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
                        .Include(c => c.Entries)
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

        public static Category DeleteCategory(Category category)
        {
            using (var db = new ExpenseDbContext())
            {
                try
                {
                    var res = db.Categories.Remove(category);
                    db.SaveChanges();
                    return res.Entity;
                } catch
                {
                    return null;
                }
            }
        }

        public static bool ImportCategories(string username, List<Category> categories)
        {
            foreach (var category in categories)
            {
                category.Username = username;
            }

            using (var db = new ExpenseDbContext())
            {
                try
                {
                    db.Categories.RemoveRange(
                        db.Categories.Where(c => c.Username == username));

                    db.Categories.AddRange(categories);
                    db.SaveChanges();
                    return true;
                } catch
                {
                    return false;
                }
            }
        }
    }
}
