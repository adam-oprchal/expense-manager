namespace DataAccessLayer
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public User User { get; set; }
        public string Username { get; set; }

        public List<Entry> Entries { get; set; }
    }
}
