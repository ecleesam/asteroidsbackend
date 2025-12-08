using asteroidsbackend.Models.Interfaces;

namespace asteroidsbackend.Data
{
    public interface IItemRepository
    {
        Task<int> AddAsync(IItem item);
        Task<List<IItem>> GetAllAsync();
        Task<IItem?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(IItem item);
        Task<bool> DeleteAsync(int id);
        Task ClearAsync();
    }
}
