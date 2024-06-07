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

            Console.WriteLine($"Began importing from {file}");
            using FileStream openStream = File.OpenRead(file);
            List<Category> categories = await JsonSerializer.
                    DeserializeAsync<List<Category>>(openStream, options);

            if (!CategoryRepository.ImportCategories(username, categories))
            {
                throw new Exception();
            }
        }
    }
}
