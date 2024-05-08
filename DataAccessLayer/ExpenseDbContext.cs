using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class ExpenseDbContext : DbContext
    {
        private string connectionString = @"server=(localdb)\MSSQLLocalDB;Initial Catalog = ExpenseDB; Integrated Security = true";

        public DbSet<User> Users { get; set; }

        public ExpenseDbContext() : base()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
