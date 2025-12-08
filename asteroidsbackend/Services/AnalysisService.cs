using asteroidsbackend.Models;
using asteroidsbackend.Models.Interfaces;
using asteroidsbackend.Data;

namespace asteroidsbackend.Services
{
    public class AnalysisService
    {
        private readonly IItemRepository _repository;

        public AnalysisService(IItemRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Dictionary<Category, int>> GetCategoryCountsAsync()
        {
            var items = await _repository.GetAllAsync();
            return items
                .GroupBy(i => i.Category)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        public async Task<List<Weapon>> GetTop5WeaponsAsync()
        {
            var items = await _repository.GetAllAsync();
            return items
                .OfType<Weapon>()
                .OrderByDescending(w => w.Damage)
                .Take(5)
                .ToList();
        }
    }
}
