using System.Text.Json;
using System.Text.Json.Serialization;
using BusinessLayer;

namespace ExpenseManager
{
    public static class Exporter
    {
        public static void Export(string username)
        {
            Console.WriteLine();

            var categories = CategoryRepository.ReadAllCategories(username);
            if (categories == null)
            {
                Console.WriteLine("Sorry, something went wrong on our side");
                return;
            }

            var options = new JsonSerializerOptions()
            {
                IncludeFields = true,
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter() }
            };

            using (StreamWriter sw = new StreamWriter(username + ".json"))
            {
                Console.WriteLine($"Began serializing to {username}.json");
                var s = JsonSerializer.Serialize(categories, options);
                sw.WriteLine(s);
            }
        }
    }
}
