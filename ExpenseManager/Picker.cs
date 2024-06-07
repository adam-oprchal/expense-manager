using BusinessLayer;
using DataAccessLayer;

namespace ExpenseManager
{
    public static class Picker
    {
        public static Category PickCategory(string username)
        {
            var categories = CategoryRepository.ReadAllCategories(username);
            if (categories == null)
            {
                Console.WriteLine("Sorry, something went wrong on our side");
                return null;
            }

            if (categories.Count == 0)
            {
                Console.WriteLine("You have no categories!");
                return null;
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
                Console.WriteLine("Incorrect category");
                return null;
            }

            return categories.ElementAt(categoryInt - 1);
        }

        public static Entry PickEntry(string username)
        {
            var entries = EntryRepository.ReadAllEntries(username);
            if (entries == null)
            {
                Console.WriteLine("Sorry, something went wrong on our side");
                return null;
            }

            if (entries.Count == 0)
            {
                Console.WriteLine("You have no entries!");
                return null;
            }

            Console.WriteLine("Your entries: ");
            for (int i = 0;  i < entries.Count; i++)
            {
                Console.WriteLine($"  {i+1} - {entries[i].Type, -15} {entries[i].Category.Name, -15} " +
                    $"{entries[i].DateTime.ToString("yyyy-MM-dd")} {entries[i].Amount, 10}   " +
                    $"{entries[i].Note}");
            }

            Console.Write("Entry: ");
            var entry = Console.ReadLine();
            if (!Int32.TryParse(entry, out int entryInt) || entryInt <= 0 
                || entryInt > entries.Count)
            {
                Console.WriteLine("Incorrect entry");
                return null;
            }

            return entries.ElementAt(entryInt - 1);
        }
    }
}
