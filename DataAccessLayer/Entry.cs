namespace DataAccessLayer
{
    public class Entry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EntryType Type { get; set; }
        public int Amount { get; set; }
        public User User { get; set; }
        public string UserName { get; set; }
    }
}
