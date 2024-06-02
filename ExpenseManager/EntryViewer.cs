using BusinessLayer;
using DataAccessLayer;

namespace ExpenseManager
{
    public static class EntryViewer
    {
        public static void ViewAllEntries(string username)
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

        public static void ViewAllEntriesFiltered(string username)
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
    }
}
