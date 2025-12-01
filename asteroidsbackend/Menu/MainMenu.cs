using asteroidsbackend.Services;

namespace asteroidsbackend.Menu
{
    public static class MainMenu
    {
        public static async void Start()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== ASTEROIDS DATA MANAGER ====");
                Console.WriteLine("1. Manage Items");
                Console.WriteLine("2. Generate Random Dataset");
                Console.WriteLine("3. Data Analysis");
                Console.WriteLine("4. Exit");
                Console.Write("\nEnter choice: ");

                switch (Console.ReadLine())
                {
                    case "1": await ItemMenu.Start(); break;
                    case "2": await RandomDataService.GenerateSampleDatasetAsync(); break;
                    case "3": await AnalysisMenu.Start(); break;
                    case "4": return;
                    default: 
                        Console.WriteLine("Invalid choice.");
                        Console.Write("Press Enter to continue...");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}
