using asteroidsbackend.Models;
using asteroidsbackend.Models.Interfaces;
using asteroidsbackend.Data;

namespace asteroidsbackend.Services
{
    public static class ItemService
    {
        public static async Task AddItem(IItem item)
        {
            await GameDatabase.DbLock.WaitAsync();
            try
            {
                item.Id = GameDatabase.NextId++;
                GameDatabase.Items.Add(item);
                Console.WriteLine("Item created");
            }
            finally
            {
                GameDatabase.DbLock.Release();
            }
        }

        public static async Task<List<IItem>> GetItems()
        {
            await GameDatabase.DbLock.WaitAsync();
            try
            {
                return GameDatabase.Items.ToList();
            }
            finally
            {
                GameDatabase.DbLock.Release();
            }
        }

        public static async Task<IItem?> GetItemById(int id)
        {
            await GameDatabase.DbLock.WaitAsync();
            try
            {
                return GameDatabase.Items.FirstOrDefault(x => x.Id == id);
            }
            finally
            {
                GameDatabase.DbLock.Release();
            }
        }

        public static async Task UpdateItem(IItem item)
        {
            await GameDatabase.DbLock.WaitAsync();
            try
            {
                var existingItem = GameDatabase.Items.FirstOrDefault(x => x.Id == item.Id);
                if (existingItem == null) { Console.WriteLine("Not found."); return; }

                // Update the item in the list
                var index = GameDatabase.Items.IndexOf(existingItem);
                GameDatabase.Items[index] = item;
                Console.WriteLine("Updated item successfully!");
            }
            finally { GameDatabase.DbLock.Release(); }
        }

        public static async Task DeleteItem(int id)
        {
            await GameDatabase.DbLock.WaitAsync();
            try
            {
                var item = GameDatabase.Items.FirstOrDefault(x => x.Id == id);
                if (item == null) { Console.WriteLine("Not found."); return; }

                GameDatabase.Items.Remove(item);
                Console.WriteLine("Deleted item");
            }
            finally { GameDatabase.DbLock.Release(); }
        }
    }
}
