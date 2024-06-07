using BusinessLayer;

namespace ExpenseManager
{
    public static class CategoryController
    {
        public static void AddCategory(string username)
        {
            Console.WriteLine();

            Console.Write($"Your new category name: ");
            var name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Category name cannot be empty");
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

        public static void EditCategory(string username)
        {
            Console.WriteLine();

            var category = Picker.PickCategory(username);
            if (category == null)
            {
                Console.WriteLine("Category edit aborted");
                return;
            }

            Console.Write("New name of this category: ");
            var newName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(newName))
            {
                Console.WriteLine("Category name cannot be empty");
                Console.WriteLine("Category edit aborted");
                return;
            }

            if (CategoryRepository.UpdateCategory(category, newName) == null)
            {
                Console.WriteLine("Sorry, something went wrong on our side");
                Console.WriteLine("Category edit aborted");
                return;
            }

            Console.WriteLine("Category successfully edited");
        }

        public static void DeleteCategory(string username)
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
    }
}
