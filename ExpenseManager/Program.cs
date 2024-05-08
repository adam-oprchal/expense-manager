using BusinessLayer;

namespace ExpenseManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var repo = new UserRepository();
            repo.AddUser("a", "a");
        }
    }
}
