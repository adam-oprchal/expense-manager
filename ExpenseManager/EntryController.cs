using BusinessLayer;
using DataAccessLayer;

namespace ExpenseManager
{
    public static class EntryController
    {
        public static void AddEntry(string username, EntryType type)
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

            var category = Picker.PickCategory(username);
            if (category == null)
            {
                Console.WriteLine($"{type} adding aborted");
                return;
            }

            Console.Write($"Your entry note (can be empty): ");
            var note = Console.ReadLine();

            var entry = EntryRepository.CreateEntry(note ?? "", amountInt, 
                category.Id, type);
            if (entry == null)
            {
                Console.WriteLine("Sorry, something went wrong on our side");
                Console.WriteLine($"{type} adding aborted");
                return;
            }

            Console.WriteLine($"{type} successfully added");
        }

        public static void EditEntry(string username)
        {
            Console.WriteLine();

            var entry = Picker.PickEntry(username);
            if (entry == null)
            {
                Console.WriteLine("Entry edit aborted");
                return;
            }

            Console.Write("New entry amount: ");
            var amount = Console.ReadLine();
            if (!Int32.TryParse(amount, out int amountInt) || amountInt <= 0)
            {
                Console.WriteLine("Entry amount has to be a positive integer");
                Console.WriteLine("Entry edit aborted");
                return;
            }

            Console.Write("Your new entry note (can be empty): ");
            var note = Console.ReadLine();

            if (EntryRepository.UpdateEntry(entry, note ?? "", amountInt) == null)
            {
                Console.WriteLine("Sorry, something went wrong on our side");
                Console.WriteLine("Entry edit aborted");
                return;
            }

            Console.WriteLine("Entry successfully edited");
        }

        public static void DeleteEntry(string username)
        {
            Console.WriteLine();

            var entry = Picker.PickEntry(username);
            if (entry == null)
            {
                Console.WriteLine("Entry deletion aborted");
                return;
            }

            if (EntryRepository.DeleteEntry(entry) == null)
            {
                Console.WriteLine("Sorry, something went wrong on our side");
                Console.WriteLine("Entry deletion aborted");
                return;
            }

            Console.WriteLine("Entry successfully deleted");
        }
    }
}
