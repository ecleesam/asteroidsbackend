using asteroidsbackend.Models;
using asteroidsbackend.Data;
using asteroidsbackend.Models.Interfaces;

namespace asteroidsbackend.Services
{
    public class RandomDataService
    {
        private readonly IItemRepository _repository;
        private readonly Random _rnd;

        public RandomDataService(IItemRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _rnd = new Random();
        }

        public async Task GenerateSampleDatasetAsync()
        {
            await _repository.ClearAsync();

            // Generate items in parallel
            var weaponTasks = new List<Task>();
            for (int i = 0; i < 10; i++)
                weaponTasks.Add(_repository.AddAsync(RandomWeapon()));

            var powerUpTasks = new List<Task>();
            for (int i = 0; i < 5; i++)
                powerUpTasks.Add(_repository.AddAsync(RandomPowerUp()));

            await Task.WhenAll(weaponTasks.Concat(powerUpTasks));
        }

        private Weapon RandomWeapon()
        {
            string[] prefixes = { "Void", "Star", "Nebula", "Cosmic", "Plasma", "Quantum", "Hyper", "Solar" };
            string[] types = { "Blaster", "Cannon", "Ray", "Repeater", "Launcher", "Beam", "Pulse" };
            
            string name = $"{prefixes[_rnd.Next(prefixes.Length)]} {types[_rnd.Next(types.Length)]}";

            return new Weapon
            {
                Name = name,
                Category = Category.Weapon,
                Damage = _rnd.Next(5, 30),
                FireRate = Math.Round(_rnd.NextDouble() * 3, 2),
                Value = _rnd.Next(10, 200),
                Weight = Math.Round(_rnd.NextDouble() * 5, 2)
            };
        }

        private PowerUp RandomPowerUp()
        {
            string[] types = { "Shield", "Speed", "Damage", "Health", "Energy" };
            string[] suffixes = { "Booster", "Amplifier", "Generator", "Cell", "Module" };

            string name = $"{types[_rnd.Next(types.Length)]} {suffixes[_rnd.Next(suffixes.Length)]}";

            return new PowerUp
            {
                Name = name,
                Category = Category.PowerUp,
                Duration = Math.Round(_rnd.NextDouble() * 30 + 5, 1),
                EffectBoost = Math.Round(_rnd.NextDouble() * 2 + 1, 1),
                Value = _rnd.Next(5, 100),
                Weight = Math.Round(_rnd.NextDouble() * 2, 2)
            };
        }
    }
}
