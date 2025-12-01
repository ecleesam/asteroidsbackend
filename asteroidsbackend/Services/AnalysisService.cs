using asteroidsbackend.Models;
using asteroidsbackend.Models.Interfaces;
using asteroidsbackend.Data;

namespace asteroidsbackend.Services
{
    public static class AnalysisService
    {
        public static async Task ShowCategoryCounts()
        {
            await GameDatabase.DbLock.WaitAsync();
            try
            {
                var result = GameDatabase.Items
                    .GroupBy(i => i.Category)
                    .Select(g => new { g.Key, Count = g.Count() })
                    .ToList();

                Console.WriteLine("\n=== Category Counts ===");
                foreach (var r in result)
                    Console.WriteLine($"{r.Key}: {r.Count}");
            }
            finally
            {
                GameDatabase.DbLock.Release();
            }
            
            Console.Write("\nPress Enter to continue...");
            Console.ReadLine();
        }

        public static async Task ShowTop5Weapons()
        {
            await GameDatabase.DbLock.WaitAsync();
            try
            {
                var top = GameDatabase.Items
                    .OfType<Weapon>()
                    .OrderByDescending(w => w.Damage)
                    .Take(5)
                    .ToList();

                Console.WriteLine("\n=== Top 5 Weapons ===");
                if (top.Count == 0)
                {
                    Console.WriteLine("No weapons found.");
                }
                else
                {
                    foreach (var w in top)
                        w.Display();
                }
            }
            finally
            {
                GameDatabase.DbLock.Release();
            }
            
            Console.Write("\nPress Enter to continue...");
            Console.ReadLine();
        }
    }
}
