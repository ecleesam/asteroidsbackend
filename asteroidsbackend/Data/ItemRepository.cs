using asteroidsbackend.Models;
using asteroidsbackend.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace asteroidsbackend.Data
{
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext _context;

        public ItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(IItem item)
        {
            if (item is not BaseItem baseItem)
            {
                throw new ArgumentException("Item must be of type BaseItem");
            }

            _context.Items.Add(baseItem);
            await _context.SaveChangesAsync();
            return baseItem.Id;
        }

        public async Task<List<IItem>> GetAllAsync()
        {
            var items = await _context.Items.ToListAsync();
            return items.Cast<IItem>().ToList();
        }

        public async Task<IItem?> GetByIdAsync(int id)
        {
            return await _context.Items.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(IItem item)
        {
            if (item is not BaseItem baseItem)
            {
                return false;
            }

            var existingItem = await _context.Items.FindAsync(item.Id);
            if (existingItem == null)
            {
                return false;
            }

            _context.Entry(existingItem).CurrentValues.SetValues(baseItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return false;
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task ClearAsync()
        {
            _context.Items.RemoveRange(_context.Items);
            await _context.SaveChangesAsync();
        }
    }
}
