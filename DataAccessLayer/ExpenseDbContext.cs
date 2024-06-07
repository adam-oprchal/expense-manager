using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class ExpenseDbContext : DbContext
    {
        private string connectionString = @"server=(localdb)\MSSQLLocalDB;Initial Catalog = ExpenseDB; Integrated Security = true";

        public DbSet<User> Users { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<Category> Categories { get; set; }

        public ExpenseDbContext() : base()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Name = "Adam", 
                    // this is a hash of '123456'
                    HashedPassword = "8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92",
                    Categories = new List<Category> { }
                }
                );

            modelBuilder.Entity<Category>().HasData(
                new Category { 
                    Id = 1,
                    Username = "Adam",
                    Name = "Work",
                    Entries = new List<Entry> { } }
                );

            modelBuilder.Entity<Entry>().HasData(
                new Entry { 
                Id = 1,
                CategoryId = 1,
                Type = EntryType.Income,
                Note = "",
                Amount = 100,
                DateTime = new DateTime(2024, 5, 7)}
                );

            modelBuilder.Entity<Entry>().HasData(
                new Entry { 
                Id = 2,
                CategoryId = 1,
                Type = EntryType.Income,
                Note = "",
                Amount = 250,
                DateTime = new DateTime(2024, 8, 7)}
                );

            modelBuilder.Entity<Category>().HasData(
                new Category { 
                    Id = 2,
                    Username = "Adam",
                    Name = "Scholarship",
                    Entries = new List<Entry> { } }
                );

            modelBuilder.Entity<Entry>().HasData(
                new Entry { 
                Id = 3,
                CategoryId = 2,
                Type = EntryType.Income,
                Note = "scholarship for good grades",
                Amount = 250,
                DateTime = new DateTime(2023, 1, 7)}
                );

            modelBuilder.Entity<Category>().HasData(
                new Category { 
                    Id = 3,
                    Username = "Adam",
                    Name = "Travel",
                    Entries = new List<Entry> { } }
                );

            modelBuilder.Entity<Entry>().HasData(
                new Entry { 
                Id = 4,
                CategoryId = 3,
                Type = EntryType.Expense,
                Note = "trip to mexico",
                Amount = 400,
                DateTime = new DateTime(2024, 1, 7)}
                );

            modelBuilder.Entity<Category>().HasData(
                new Category { 
                    Id = 4,
                    Username = "Adam",
                    Name = "Food",
                    Entries = new List<Entry> { } }
                );

            modelBuilder.Entity<Entry>().HasData(
                new Entry { 
                Id = 5,
                CategoryId = 4,
                Type = EntryType.Expense,
                Note = "kfc spicy chicken burger",
                Amount = 20,
                DateTime = new DateTime(2024, 4, 7)}
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
