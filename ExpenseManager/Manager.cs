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
                Console.WriteLine("Choose your action: ");
                Console.WriteLine("  add-expense");
                Console.WriteLine("  add-income");
                Console.WriteLine("  add-category");
                Console.WriteLine("  view-all-entries");
                Console.WriteLine("  view-all-entries-filtered");
                Console.WriteLine("  log-out");

                Console.Write($"[{username}]> ");
                input = (Console.ReadLine() ?? "").Trim();

                switch (input)
                {
                    case "add-expense":
                        AddEntry(EntryType.Expense);
                        break;
                    case "add-income":
                        AddEntry(EntryType.Income);
                        break;
                    case "add-category":
                        AddCategory();
                        break;
                    case "view-all-entries":
                        ViewAllEntries();
                        break;
                    case "view-all-entries-filtered":
                        ViewAllEntriesFiltered();
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

            Console.Write($"{type} amount: ");
            var amount = Console.ReadLine();
            if (!Int32.TryParse(amount, out int amountInt) || amountInt <= 0)
            {
                Console.WriteLine($"{type} amount has to be a positive integer");
                Console.WriteLine($"{type} adding aborted");
                return;
            }

            var categories = CategoryRepository.ReadAllCategories(username);
            if (categories == null)
            {
                Console.WriteLine("Sorry, something went wrong on our side");
                Console.WriteLine($"{type} adding aborted");
                return;
            }

            Console.WriteLine("Your categories: ");
            for (int i = 0;  i < categories.Count; i++)
            {
                Console.WriteLine($"  {i+1} - {categories[i].Name}");
            }

            Console.Write("Category: ");
            var category = Console.ReadLine();
            if (!Int32.TryParse(category, out int categoryInt) || categoryInt <= 0 
                || categoryInt > categories.Count)
            {
                Console.WriteLine($"Incorrect category");
                Console.WriteLine($"{type} adding aborted");
                return;
            }

            Console.Write($"Your entry note (can be empty): ");
            var note = Console.ReadLine();

            var entry = EntryRepository.CreateEntry(note ?? "", amountInt, 
                categories[categoryInt - 1].Id, type);
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

            var entries = EntryRepository.ReadAllEntries(username);
            if (entries == null)
            {
                Console.WriteLine("Sorry, something went wrong on our side");
                return;
            }

            Console.WriteLine("Your current entries: ");
            Console.WriteLine($"{"Type", -15} {"Category", -15} {"Amount", 10}   {"Note"}");
            Console.WriteLine("==========================================================");
            foreach (var entry in entries)
            {
                Console.WriteLine($"{entry.Type, -15} {entry.Category.Name, -15} " +
                    $"{entry.Amount, 10}   {entry.Note}");
            }
            Console.WriteLine("==========================================================");

            var income = entries
                .Where(e => e.Type == EntryType.Income)
                .Select(e => e.Amount)
                .Sum();
            var expense = entries
                .Where(e => e.Type == EntryType.Expense)
                .Select(e => e.Amount)
                .Sum();

            Console.WriteLine($"Total income: {income, 28}");
            Console.WriteLine($"Total expenses: {expense, 26}");
            Console.WriteLine("==========================================================");
            Console.WriteLine($"Total balance: {income - expense, 27}");
        }

        private void ViewAllEntriesFiltered()
        {
            Console.WriteLine();

            var entries = EntryRepository.ReadAllEntries(username);
            if (entries == null)
            {
                Console.WriteLine("Sorry, something went wrong on our side");
                return;
            }

            Console.WriteLine("Filters: ");
            Console.WriteLine("  1 - Category filter");
            Console.WriteLine("  2 - Month filter");
            Console.WriteLine("  3 - Year filter");

            Console.Write("Filter: ");
            var filter = (Console.ReadLine() ?? "").Trim();

            switch (filter)
            {
                case "1":
                    Console.WriteLine($"{filter}");
                    break;
                case "2":
                    Console.WriteLine($"{filter}");
                    break;
                case "3":
                    Console.WriteLine($"{filter}");
                    break;
                default:
                    Console.WriteLine("Incorrect filter");
                    break;
            }
        }

        private void AddCategory()
        {
            Console.WriteLine();

            var categories = CategoryRepository.ReadAllCategories(username);
            if (categories == null)
            {
                Console.WriteLine("Sorry, something went wrong on our side");
                Console.WriteLine("Category adding aborted");
                return;
            }

            Console.Write($"Your new category name: ");
            var name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Category name cannot be empty");
                Console.WriteLine("Category adding aborted");
                return;
            }

            if (categories.Select(c => c.Name).Contains(name))
            {
                Console.WriteLine("Category name is already in use");
                Console.WriteLine("Category adding aborted");
                return;
            }

            var category = CategoryRepository.CreateCategory(name, username);
            if (category == null)
            {
                Console.WriteLine("Sorry, something went wrong on our side");
                Console.WriteLine("Category adding aborted");
                return;
            }

            Console.WriteLine("Category successfully added");
        }
    }
}
