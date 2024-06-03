using System.Text.Json.Serialization;

namespace DataAccessLayer
{
    public class Entry
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Note { get; set; }
        public EntryType Type { get; set; }
        public int Amount { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;

        [JsonIgnore]
        public Category Category { get; set; }
        [JsonIgnore]
        public int CategoryId { get; set; }
    }
}
