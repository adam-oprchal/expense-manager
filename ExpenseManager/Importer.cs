namespace ExpenseManager
{
    public static class Importer
    {
        public static void Import(string username)
        {
            Console.Write("Choose import file: ");
            var file = (Console.ReadLine() ?? "").Trim();

            if (!File.Exists(file))
            {
                Console.WriteLine("Sorry, file doesn't exist!");
                return;
            }

            using (StreamReader sr = new StreamReader(file))
            {
                var s = sr.ReadToEnd();
                Console.WriteLine(s);
            }
        }
    }
}
