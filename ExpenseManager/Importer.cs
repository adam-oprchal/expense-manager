using System.Text.Json;
using System.Text.Json.Serialization;
using BusinessLayer;
using DataAccessLayer;

namespace ExpenseManager
{
    public static class Importer
    {
        public static void Import(string username)
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

            using (StreamReader sr = new StreamReader(file))
            {
                var s = sr.ReadToEnd();

                List<Category> categories;
                try
                {
                    categories = JsonSerializer.Deserialize<List<Category>>(s, options);
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
                }
            }
        }
    }
}
