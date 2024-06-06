using BusinessLayer;
using DataAccessLayer;
using Microsoft.Identity.Client;

namespace ExpenseManager
{
    public static class EntryViewer
    {
        private static void PrintEntries(List<Entry> entries)
        {
            Console.WriteLine("Your current entries: ");
            Console.WriteLine($"{"Type", -15} {"Category", -15} {"Date", -10} {"Amount", 10}   {"Note"}");
            Console.WriteLine("=============================================================");
            foreach (var entry in entries)
            {
                Console.WriteLine($"{entry.Type, -15} {entry.Category.Name, -15} " +
                    $"{entry.DateTime.ToString("yyyy-MM-dd")} {entry.Amount, 10}   " +
                    $"{entry.Note}");
            }
            Console.WriteLine("=============================================================");

            var income = entries
                .Where(e => e.Type == EntryType.Income)
                .Select(e => e.Amount)
                .Sum();
            var expense = entries
                .Where(e => e.Type == EntryType.Expense)
                .Select(e => e.Amount)
                .Sum();

            Console.WriteLine($"Total income: {income, 39}");
            Console.WriteLine($"Total expenses: {expense, 37}");
            Console.WriteLine("=============================================================");
            Console.WriteLine($"Total balance: {income - expense, 38}");
        }

        public static void ViewAllEntries(string username)
        {
            Console.WriteLine();

            var entries = EntryRepository.ReadAllEntries(username);
            if (entries == null)
            {
                Console.WriteLine("Sorry, something went wrong on our side");
                return;
            }
            PrintEntries(entries);
        }

        private static void ViewAllEntriesFilteredCategory(
            string username, List<Entry> entries)
        {
            var category = Picker.PickCategory(username);
            if (category == null) return;

            entries = entries
                .Where(e => e.CategoryId == category.Id)
                .ToList();

            PrintEntries(entries);
        }

        private static void ViewAllEntriesFilteredMonth(List<Entry> entries)
        {
            Console.WriteLine("Pick month: 1 = January, 12 = December");
            Console.Write("Month: ");

            var month = Console.ReadLine();
            if (!Int32.TryParse(month, out int monthInt) || monthInt <= 0 
                || monthInt > 12)
            {
                Console.WriteLine("Incorrect month");
                return;
            }

            entries = entries
                .Where(e => e.DateTime.Month == monthInt)
                .ToList();

            PrintEntries(entries);
        }

        private static void ViewAllEntriesFilteredYear(List<Entry> entries)
        {
            Console.Write("Pick year: ");

            var year = Console.ReadLine();
            if (!Int32.TryParse(year, out int yearInt))
            {
                Console.WriteLine("Incorrect year");
                return;
            }

            entries = entries
                .Where(e => e.DateTime.Year == yearInt)
                .ToList();

            PrintEntries(entries);
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
                    ViewAllEntriesFilteredCategory(username, entries);
                    break;
                case "2":
                    ViewAllEntriesFilteredMonth(entries);
                    break;
                case "3":
                    ViewAllEntriesFilteredYear(entries);
                    break;
                default:
                    Console.WriteLine("Incorrect filter");
                    break;
            }
        }
    }
}
