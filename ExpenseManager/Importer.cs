using System.Text.Json;
using System.Text.Json.Serialization;
using BusinessLayer;
using DataAccessLayer;

namespace ExpenseManager
{
    public static class Importer
    {
        public static async Task Import(string username)
        {
            Console.WriteLine();

            Console.WriteLine("WARNING: this action will delete all your current data");
            Console.WriteLine("If you want to continue, type YES");
            var choice = (Console.ReadLine() ?? "").Trim();
            if (choice != "YES")
            {
                Console.WriteLine("Importing aborted");
                return;
            }

            Console.Write("Choose import file: ");
            var file = (Console.ReadLine() ?? "").Trim();

            if (!File.Exists(file))
            {
                Console.WriteLine("Sorry, file doesn't exist!");
                return;
            }

            var options = new JsonSerializerOptions()
            {
                IncludeFields = true,
                Converters = { new JsonStringEnumConverter() }
            };

            using FileStream openStream = File.OpenRead(file);
            List<Category> categories;
            try
            {
                categories = await JsonSerializer.
                    DeserializeAsync<List<Category>>(openStream, options);
            } catch
            {
                Console.WriteLine("Invalid file");
                return;
            }

            if (categories == null)
            {
                Console.WriteLine("Invalid file");
                return;
            }

            if (!CategoryRepository.ImportCategories(username, categories))
            {
                Console.WriteLine("Sorry, something went wrong on our side");
                return;
            }
            Console.WriteLine("Data successfully imported");
        }
    }
}
