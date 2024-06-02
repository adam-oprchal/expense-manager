using DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer
{
    public static class EntryRepository
    {
        public static Entry CreateEntry(string note, int amount, 
            int categoryId, EntryType type)
        {
            using (var db = new ExpenseDbContext())
            {
                try
                {
                    var res = db.Entries.Add(
                        new Entry 
                        { Note = note, Amount = amount, 
                            CategoryId = categoryId, Type = type });
                    db.SaveChanges();
                    return res.Entity;
                } catch
                {
                    return null;
                }
            }
        }

        public static List<Entry> ReadAllEntries(string username)
        {
            using (var db = new ExpenseDbContext())
            {
                try
                {
                    return db.Entries
                        .Where(e => e.Category.Username == username)
                        .Include(e => e.Category)
                        .ToList();
                } catch
                {
                    return null;
                }
            }
        }
    }
}
