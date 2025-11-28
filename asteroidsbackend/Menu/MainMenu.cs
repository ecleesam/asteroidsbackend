using asteroidsbackend.Services;

namespace asteroidsbackend.Menu
{
    public static class MainMenu
    {
        public static void Start()
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
                    case "1": ItemMenu.Start(); break;
                    case "2": RandomDataService.GenerateSampleDataset(); break;
                    case "3": AnalysisMenu.Start(); break;
                    case "4": return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}