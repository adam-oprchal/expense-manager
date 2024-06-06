using BusinessLayer;

namespace ExpenseManager
{
    public class Program
    {
        static void Main()
        {
            Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
            Console.WriteLine("$$                                 $$");
            Console.WriteLine("$$   Welcome to Expense Manager!   $$");
            Console.WriteLine("$$                                 $$");
            Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");

            string input;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Choose your action: log-in, register, exit");
                Console.Write("> ");
                input = (Console.ReadLine() ?? "").Trim();

                switch (input)
                {
                    case "log-in":
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
            Console.WriteLine();
            Console.WriteLine("Please enter your username and password");

            Console.Write("Username: ");
            var username = Console.ReadLine();
            Console.Write("Password: ");
            var password = PasswordReader.Read();

            if (username == null)
            {
                Console.WriteLine("Incorrect username or password");
                return;
            }

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
            Console.WriteLine();

            Console.Write("Choose your username: ");
            var username = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(username))
            {
                Console.WriteLine("Username cannot be empty");
                Console.WriteLine("Aborting registration");
                return;
            }

            Console.Write("Choose your password: ");
            var password = PasswordReader.Read();
            if (password == "")
            {
                Console.WriteLine("Password cannot be empty");
                Console.WriteLine("Aborting registration");
                return;
            }

            Console.Write("Repeat password: ");
            var passwordAgain = PasswordReader.Read();
            if (password != passwordAgain)
            {
                Console.WriteLine("Passwords don't match");
                Console.WriteLine("Aborting registration");
                return;
            }

            var user = UserRepository.CreateUser(username, password);
            if (user == null)
            {
                Console.WriteLine("Username is already taken");
                Console.WriteLine("Aborting registration");
                return;
            }

            Console.WriteLine("Registration was successful, " +
                "please log in to continue to your account");
        }
    }
}
