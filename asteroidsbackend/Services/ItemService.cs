using asteroidsbackend.Models;
using asteroidsbackend.Models.Interfaces;
using asteroidsbackend.Data;

namespace asteroidsbackend.Services
{
    public class ItemService
    {
        private readonly IItemRepository _repository;

        public ItemService(IItemRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<int> AddItemAsync(IItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Item cannot be null");

            return await _repository.AddAsync(item);
        }

        public async Task<List<IItem>> GetAllItemsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IItem?> GetItemByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateItemAsync(IItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Item cannot be null");

            return await _repository.UpdateAsync(item);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
