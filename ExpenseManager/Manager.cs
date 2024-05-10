using BusinessLayer;
using DataAccessLayer;

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
                Console.WriteLine("Choose your action: add-expense, add-income, " +
                    "view-all-entries, log-out");

                Console.Write($"[{username}]> ");
                input = Console.ReadLine().Trim();

                switch (input)
                {
                    case "add-expense":
                        AddEntry(EntryType.Expense);
                        break;
                    case "add-income":
                        AddEntry(EntryType.Income);
                        break;
                    case "view-all-entries":
                        ViewAllEntries();
                        break;
                    case "log-out":
                        Console.WriteLine($"Goodbye, {username}!");
                        break;
                    default:
                        Console.WriteLine("Incorrect action");
                        break;
                }
            } while (input != "log-out");
        }

        private void AddEntry(EntryType type)
        {
            Console.WriteLine();

            Console.Write($"{type} name: ");
            var name = Console.ReadLine();
            if (name == "")
            {
                Console.WriteLine($"{type} name cannot be empty");
                Console.WriteLine($"{type} adding aborted");
                return;
            }

            Console.Write($"{type} amount: ");
            var amount = Console.ReadLine();
            if (!Int32.TryParse(amount, out int amountInt) || amountInt <= 0)
            {
                Console.WriteLine($"{type} amount has to be a positive integer");
                Console.WriteLine($"{type} adding aborted");
                return;
            }

            var entry = EntryRepository.AddEntry(name, amountInt, username, type);
            if (entry == null)
            {
                Console.WriteLine("Sorry, something went wrong on our side");
                Console.WriteLine($"{type} adding aborted");
                return;
            }

            Console.WriteLine($"{type} successfully added");
        }

        private void ViewAllEntries()
        {
            Console.WriteLine();

            var entries = EntryRepository.GetAllEntries(username);
            if (entries == null)
            {
                Console.WriteLine("Sorry, something went wrong on our side");
                return;
            }

            Console.WriteLine("Your current entries: ");
            Console.WriteLine("======================================");
            foreach (var entry in entries)
            {
                Console.WriteLine($"{entry.Name, -30} {entry.Type, -10} {entry.Amount}");
            }
            Console.WriteLine("======================================");

            var income = entries
                .Where(e => e.Type == EntryType.Income)
                .Select(e => e.Amount)
                .Sum();
            var expense = entries
                .Where(e => e.Type == EntryType.Expense)
                .Select(e => e.Amount)
                .Sum();

            Console.WriteLine($"Total income: {income}");
            Console.WriteLine($"Total expenses: {expense}");
            Console.WriteLine("======================================");
            Console.WriteLine($"Total balance: {income - expense}");
        }
    }
}
