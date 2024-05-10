using DataAccessLayer;

namespace BusinessLayer
{
    public static class EntryRepository
    {
        public static Entry AddEntry(string name, int amount, 
            string username, EntryType type)
        {
            using (var db = new ExpenseDbContext())
            {
                try
                {
                    var res = db.Entries.Add(
                        new Entry 
                        { Name = name, Amount = amount, 
                            UserName = username, Type = type });
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
