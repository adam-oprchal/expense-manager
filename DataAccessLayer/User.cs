using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer
{
    public class User
    {
        [Key]
        public string Name { get; set; }
        public string HashedPassword { get; set; }
    }
}
