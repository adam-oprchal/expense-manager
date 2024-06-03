using System.Text.Json;
using System.Text.Json.Serialization;
using BusinessLayer;

namespace ExpenseManager
{
    public static class Exporter
    {
        public static async void Export(string username)
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

            Console.WriteLine($"Began serializing to {username}.json");
            await using FileStream createStream = File.Create(username + ".json");
            await JsonSerializer.SerializeAsync(createStream, categories, options);
        }
    }
}
