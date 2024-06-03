using System.Text.Json.Serialization;

namespace DataAccessLayer
{
    public class Category
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public User User { get; set; }
        [JsonIgnore]
        public string Username { get; set; }

        public List<Entry> Entries { get; set; }
    }
}
