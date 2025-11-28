using asteroidsbackend.Services;

namespace asteroidsbackend.Menu;

public static class AnalysisMenu
{
    public static void Start()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("==== DATA ANALYSIS ====");
            Console.WriteLine("1. Count by Category");
            Console.WriteLine("2. Average Stats (Damage / Duration)");
            Console.WriteLine("3. Top 5 Strongest Weapons");
            Console.WriteLine("4. Back");
            Console.Write("\nEnter choice: ");

            switch (Console.ReadLine())
            {
                case "1": AnalysisService.ShowCategoryCounts(); break;
                case "2": AnalysisService.ShowAverages(); break;
                case "3": AnalysisService.ShowTop5Weapons(); break;
                case "4": return;
                default:
                    Console.WriteLine("Invalid choice.");
                    Console.ReadLine();
                    break;
            }
        }
    }
}
