using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer
{
    public class Entry
    {
        public int Id { get; set; }
        public string Note { get; set; }
        public EntryType Type { get; set; }
        public int Amount { get; set; }

        public Category Category { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
    }
}
