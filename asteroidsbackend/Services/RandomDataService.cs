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
            return new Weapon
            {
                Name = "Weapon " + _rnd.Next(100),
                Category = Category.Weapon,
                Damage = _rnd.Next(5, 30),
                FireRate = Math.Round(_rnd.NextDouble() * 3, 2),
                Value = _rnd.Next(10, 200),
                Weight = Math.Round(_rnd.NextDouble() * 5, 2)
            };
        }

        private PowerUp RandomPowerUp()
        {
            return new PowerUp
            {
                Name = "PowerUp " + _rnd.Next(100),
                Category = Category.PowerUp,
                Duration = _rnd.Next(3, 12),
                EffectBoost = Math.Round(_rnd.NextDouble() * 5, 2),
                Value = _rnd.Next(5, 100),
                Weight = Math.Round(_rnd.NextDouble() * 1.5, 2)
            };
        }
    }
}
