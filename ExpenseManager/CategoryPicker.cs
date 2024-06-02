using BusinessLayer;
using DataAccessLayer;

namespace ExpenseManager
{
    public static class CategoryPicker
    {
        public static Category PickCategory(string username)
        {
            var categories = CategoryRepository.ReadAllCategories(username);
            if (categories == null)
            {
                Console.WriteLine("Sorry, something went wrong on our side");
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
    }
}
