using asteroidsbackend.Models;
using asteroidsbackend.Data;

namespace asteroidsbackend.Services
{
    public static class RandomDataService
    {
        private static readonly Random rnd = new Random();

        public static async Task GenerateSampleDatasetAsync()
        {
            Console.WriteLine("Generating dataset...");

            await GameDatabase.DbLock.WaitAsync();
            try
            {
                GameDatabase.Items.Clear();
                GameDatabase.NextId = 1;
            }
            finally
            {
                GameDatabase.DbLock.Release();
            }

            // Generate items in parallel2
            var weaponTasks = new List<Task>();
            for (int i = 0; i < 10; i++)
                weaponTasks.Add(ItemService.AddItem(RandomWeapon()));

            var powerUpTasks = new List<Task>();
            for (int i = 0; i < 5; i++)
                powerUpTasks.Add(ItemService.AddItem(RandomPowerUp()));

            await Task.WhenAll(weaponTasks.Concat(powerUpTasks));

            Console.WriteLine("Random dataset generated!");
            Console.Write("Press Enter to continue...");
            Console.ReadLine();
        }

        private static Weapon RandomWeapon()
        {
            return new Weapon
            {
                Name = "Weapon " + rnd.Next(100),
                Category = Category.Weapon,
                Damage = rnd.Next(5, 30),
                FireRate = Math.Round(rnd.NextDouble() * 3, 2),
                Value = rnd.Next(10, 200),
                Weight = Math.Round(rnd.NextDouble() * 5, 2)
            };
        }

        private static PowerUp RandomPowerUp()
        {
            return new PowerUp
            {
                Name = "PowerUp " + rnd.Next(100),
                Category = Category.PowerUp,
                Duration = rnd.Next(3, 12),
                EffectBoost = Math.Round(rnd.NextDouble() * 5, 2),
                Value = rnd.Next(5, 100),
                Weight = Math.Round(rnd.NextDouble() * 1.5, 2)
            };
        }
    }
}
