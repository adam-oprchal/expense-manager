using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer
{
    public class User
    {
        [Key]
        public string Name { get; set; }
        public string HashedPassword { get; set; }

        public List<Category> Categories { get; set; } = 
            new List<Category>() {
                new Category() { Name = "Education" },
                new Category() { Name = "Travel" },
                new Category() { Name = "Work"} 
            };
    }
}
