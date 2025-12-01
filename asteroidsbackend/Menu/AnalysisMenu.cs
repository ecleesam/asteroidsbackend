using asteroidsbackend.Services;

namespace asteroidsbackend.Menu;

public static class AnalysisMenu
{
    public static async Task Start()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("==== DATA ANALYSIS ====");
            Console.WriteLine("1. Count by Category");
            Console.WriteLine("2. Top 5 Strongest Weapons");
            Console.WriteLine("3. Back");
            Console.Write("\nEnter choice: ");

            switch (Console.ReadLine())
            {
                case "1": await AnalysisService.ShowCategoryCounts(); break;
                case "2": await AnalysisService.ShowTop5Weapons(); break;
                case "3": return;
                default:
                    Console.WriteLine("Invalid choice.");
                    Console.Write("Press Enter to continue...");
                    Console.ReadLine();
                    break;
            }
        }
    }
}
