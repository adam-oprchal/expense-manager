using BusinessLayer;
using DataAccessLayer;
using ScottPlot;

namespace ExpenseManager
{
    public static class Grapher
    {
        private static Random rnd = new Random();

        public static void ExportStatistics(string username)
        {
            Console.WriteLine();

            var categories = CategoryRepository.ReadAllCategories(username);
            if (categories == null)
            {
                Console.WriteLine("Sorry, something went wrong on our side");
                return;
            }

            PlotPieChart(EntryType.Income, categories);
            PlotPieChart(EntryType.Expense, categories);
            PlotBar(categories);

            Console.WriteLine("Successfully created and exported basic plots!");
            Console.WriteLine("Created: pieIncome.png, pieExpense.png and totalBarPlot.png");
        }

        private static void PlotPieChart(EntryType entryType, List<Category> categories)
        {
            var sums = categories
                .Select(c => new { 
                    c.Name, 
                    Sum = c.Entries
                        .Where(e => e.Type == entryType)
                        .Select(e => e.Amount).Sum(),
                });

            List<PieSlice> slices = sums
                .Where(c => c.Sum != 0)
                .Select(c => new PieSlice {Value = c.Sum, Label = c.Name, FillColor = RandomColor()})
                .ToList();

            ScottPlot.Plot plot = new ScottPlot.Plot();

            var pie = plot.Add.Pie(slices);

            plot.ShowLegend();
            plot.Add.Annotation($"Total {entryType} by Category");

            plot.SavePng($"pie{entryType}.png", 600, 600);
        }

        private static void PlotBar(List<Category> categories)
        {
            ScottPlot.Plot plot = new ScottPlot.Plot();

            var bars1 = plot.Add.Bars(
                [0],
                [ categories
                .SelectMany(c => c.Entries)
                .Where(e => e.Type == EntryType.Income)
                .Select(e => e.Amount)
                .Sum() ]);
            bars1.LegendText = "Income";

            var bars2 = plot.Add.Bars( 
                [1],
                [ categories
                .SelectMany(c => c.Entries)
                .Where(e => e.Type == EntryType.Expense)
                .Select(e => e.Amount)
                .Sum() ]);
            bars2.LegendText = "Expense";

            plot.ShowLegend();
            plot.Add.Annotation("Total Income and Expense");

            plot.SavePng("totalBarPlot.png", 600, 600);
        }

        private static Color RandomColor()
        {
            return new Color(rnd.Next(256), rnd.Next(256), rnd.Next(256));
        }
    }
}
