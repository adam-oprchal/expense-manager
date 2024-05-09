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
                        Register();
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

        static void Register()
        {
            Console.Write("Choose your username: ");
            var username = Console.ReadLine();
            if (username == "")
            {
                Console.WriteLine("Username cannot be empty");
                Console.WriteLine("Aborting registration");
                return;
            }

            Console.Write("Choose your password: ");
            var password = Console.ReadLine();
            if (password == "")
            {
                Console.WriteLine("Password cannot be empty");
                Console.WriteLine("Aborting registration");
                return;
            }

            var user = UserRepository.AddUser(username, password);
            if (user == null)
            {
                Console.WriteLine("Username is already taken");
                Console.WriteLine("Aborting registration");
                return;
            }

            Console.WriteLine("Your account was successfully created");
        }
    }
}
