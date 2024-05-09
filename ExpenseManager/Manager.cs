namespace ExpenseManager
{
    public class Manager
    {
        private string username { get; set; }

        public Manager(string username) { this.username = username; }

        public void Start()
        {
            Console.WriteLine($"Welcome back, {username}!");

            string input;
            do
            {
                Console.WriteLine("Choose your action: log-out");
                input = Console.ReadLine().Trim();

                switch (input)
                {
                    case "log-out":
                        Console.WriteLine($"Goodbye, {username}!");
                        break;
                    default:
                        Console.WriteLine("Incorrect command");
                        break;
                }
            } while (input != "log-out");
        }
    }
}
