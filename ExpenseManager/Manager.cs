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
                Console.WriteLine("  delete-category");
                Console.WriteLine("  view-all-entries");
                Console.WriteLine("  view-all-entries-filtered");
                Console.WriteLine("  export-data");
                Console.WriteLine("  import-data");
                Console.WriteLine("  show-statistics");
                Console.WriteLine("  delete-account");
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
                    case "delete-category":
                        DeleteCategory();
                        break;
                    case "view-all-entries":
                        EntryViewer.ViewAllEntries(username);
                        break;
                    case "view-all-entries-filtered":
                        EntryViewer.ViewAllEntriesFiltered(username);
                        break;
                    case "export-data":
                        Exporter.Export(username)
                            .ContinueWith(t => { 
                                Console.WriteLine("\nSorry, something went wrong with the export"); 
                            }, TaskContinuationOptions.OnlyOnFaulted);
                        break;
                    case "import-data":
                        Importer.Import(username).Wait();
                        break;
                    case "show-statistics":
                        Grapher.ExportStatistics(username);
                        break;
                    case "delete-account":
                        if (DeleteAccount()) return;
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

        private void DeleteCategory()
        {
            Console.WriteLine();

            var category = Picker.PickCategory(username);
            if (category == null)
            {
                Console.WriteLine("Category deletion aborted");
                return;
            }

            if (CategoryRepository.DeleteCategory(category) == null)
            {
                Console.WriteLine("Sorry, something went wrong on our side");
                Console.WriteLine("Category deletion aborted");
                return;
            }

            Console.WriteLine("Category successfully deleted");
        }

        private bool DeleteAccount()
        {
            Console.WriteLine("WARNING: this action will delete all your current data");
            Console.WriteLine("If you want to continue, type YES");
            var choice = (Console.ReadLine() ?? "").Trim();
            if (choice != "YES")
            {
                Console.WriteLine("Deletion aborted");
                return false;
            }

            if (UserRepository.DeleteUser(username) == null)
            {
                Console.WriteLine("Sorry, something went wrong on our side");
                Console.WriteLine("Deletion aborted");
                return false;
            }

            Console.WriteLine("Successfully deleted your account!");
            return true;
        }
    }
}
