using asteroidsbackend.Models;
using asteroidsbackend.Models.Interfaces;
using asteroidsbackend.Data;

namespace asteroidsbackend.Services
{
    public static class ItemService
    {
        public static async Task AddItem(IItem item)
        {
            try
            {
                if (item == null)
                    throw new ArgumentNullException(nameof(item), "Item cannot be null");

                await GameDatabase.DbLock.WaitAsync();
                try
                {
                    item.Id = GameDatabase.NextId++;
                    GameDatabase.Items.Add(item);
                    Console.WriteLine("Item created successfully!");
                }
                finally
                {
                    GameDatabase.DbLock.Release();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding item: {ex.Message}");
                throw;
            }
        }

        public static async Task<List<IItem>> GetItems()
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving items: {ex.Message}");
                return new List<IItem>();
            }
        }

        public static async Task<IItem?> GetItemById(int id)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving item: {ex.Message}");
                return null;
            }
        }

        public static async Task UpdateItem(IItem item)
        {
            try
            {
                if (item == null)
                    throw new ArgumentNullException(nameof(item), "Item cannot be null");

                await GameDatabase.DbLock.WaitAsync();
                try
                {
                    var existingItem = GameDatabase.Items.FirstOrDefault(x => x.Id == item.Id);
                    if (existingItem == null) 
                    { 
                        Console.WriteLine("Item not found."); 
                        return; 
                    }

                    // Update the item in the list
                    var index = GameDatabase.Items.IndexOf(existingItem);
                    GameDatabase.Items[index] = item;
                    Console.WriteLine("Updated item successfully!");
                }
                finally 
                { 
                    GameDatabase.DbLock.Release(); 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating item: {ex.Message}");
                throw;
            }
        }

        public static async Task DeleteItem(int id)
        {
            try
            {
                await GameDatabase.DbLock.WaitAsync();
                try
                {
                    var item = GameDatabase.Items.FirstOrDefault(x => x.Id == id);
                    if (item == null) 
                    { 
                        Console.WriteLine("Item not found."); 
                        return; 
                    }

                    GameDatabase.Items.Remove(item);
                    Console.WriteLine("Deleted item successfully!");
                }
                finally 
                { 
                    GameDatabase.DbLock.Release(); 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting item: {ex.Message}");
                throw;
            }
        }
    }
}
