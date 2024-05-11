using DataAccessLayer;

namespace BusinessLayer
{
    public static class CategoryRepository
    {
        public static List<Category> GetAllCategories(string username)
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
    }
}
