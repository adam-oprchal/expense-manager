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

        public static Entry UpdateEntry(Entry entry, 
            string newNote, int newAmount)
        {
            using (var db = new ExpenseDbContext())
            {
                try
                {
                    var res = db.Entries.Single(e => e == entry);
                    res.Note = newNote;
                    res.Amount = newAmount;
                    db.SaveChanges();
                    return res;
                } catch
                {
                    return null;
                }
            }
        }

        public static Entry DeleteEntry(Entry entry)
        {
            using (var db = new ExpenseDbContext())
            {
                try
                {
                    var res = db.Entries.Remove(entry);
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
