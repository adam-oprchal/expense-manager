using BusinessLayer;

namespace ExpenseManager
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Expense Manager!");

            string input;
            do
            {
                Console.WriteLine("Choose your action: login, register, exit");
                input = Console.ReadLine().Trim();

                switch (input)
                {
                    case "login":
                        Login();
                        break;
                    case "register":
                        break;
                    case "exit":
                        Console.WriteLine("Exiting");
                        break;
                    default:
                        Console.WriteLine("Incorrect action");
                        break;
                }
            } while (input != "exit");
        }

        static void Login()
        {
            Console.Write("Username: ");
            var username = Console.ReadLine();
            Console.Write("Password: ");
            var password = Console.ReadLine();

            var user = UserRepository.ValidateLogin(username, password);
            if (user == null)
            {
                Console.WriteLine("Incorrect username or password");
                return;
            }

            var manager = new Manager(user.Name);
            manager.Start();
        }
    }
}
