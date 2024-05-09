namespace ExpenseManager
{
    public class Manager
    {
        private string username { get; set; }

        public Manager(string username) { this.username = username; }

        public void Start()
        {
            Console.WriteLine();
            Console.WriteLine($"Welcome, {username}!");

            string input;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Choose your action: log-out");
                Console.Write($"[{username}]> ");
                input = Console.ReadLine().Trim();

                switch (input)
                {
                    case "log-out":
                        Console.WriteLine($"Goodbye, {username}!");
                        break;
                    default:
                        Console.WriteLine("Incorrect action");
                        break;
                }
            } while (input != "log-out");
        }
    }
}
