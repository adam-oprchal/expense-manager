using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer
{
    public class Entry
    {
        public int Id { get; set; }
        public string Note { get; set; }
        public EntryType Type { get; set; }
        public int Amount { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;

        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
