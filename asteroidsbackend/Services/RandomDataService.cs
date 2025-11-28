using asteroidsbackend.Models;
using asteroidsbackend.Data;

namespace asteroidsbackend.Services
{
    public static class RandomDataService
    {
        private static Random rnd = new Random();

        public static void GenerateSampleDataset()
        {
            GameDatabase.Items.Clear();
            GameDatabase.NextId = 1;

            for (int i = 0; i < 10; i++)
                GameDatabase.Items.Add(RandomWeapon());

            for (int i = 0; i < 5; i++)
                GameDatabase.Items.Add(RandomPowerUp());

            Console.WriteLine("Random dataset generated.");
            Console.ReadLine();
        }

        private static Weapon RandomWeapon()
        {
            return new Weapon
            {
                Id = GameDatabase.NextId++,
                Name = "Weapon " + rnd.Next(100),
                Category = Category.Weapon,
                Damage = rnd.Next(5, 30),
                FireRate = rnd.NextDouble() * 3
            };
        }

        private static PowerUp RandomPowerUp()
        {
            return new PowerUp
            {
                Id = GameDatabase.NextId++,
                Name = "PowerUp " + rnd.Next(100),
                Category = Category.PowerUp,
                Duration = rnd.Next(5, 20),
                EffectBoost = rnd.NextDouble() * 2
            };
        }
    }
}