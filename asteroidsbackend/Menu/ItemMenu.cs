using asteroidsbackend.Services;
using asteroidsbackend.Models;

namespace asteroidsbackend.Menu;

public static class ItemMenu
{
    public static async Task Start()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("===ITEM MANAGEMENT===");
            Console.WriteLine("1. Add Item");
            Console.WriteLine("2. View All Items");
            Console.WriteLine("3. Update Item");
            Console.WriteLine("4. Delete Item");
            Console.WriteLine("5. Return to Main Menu");
            Console.Write("\nEnter choice: ");

            switch (Console.ReadLine())
            {
                case "1": await AddItemMenu(); break;
                case "2": await ViewItemsMenu(); break;
                case "3": await UpdateItemMenu(); break;
                case "4": await DeleteItemMenu(); break;
                case "5": return;
                default:
                    Console.WriteLine("Invalid choice.");
                    Console.Write("Press Enter to continue...");
                    Console.ReadLine();
                    break;
            }
        }
    }

    private static async Task AddItemMenu()
    {
        try
        {
            Console.WriteLine("\n1. Add Weapon");
            Console.WriteLine("2. Add PowerUp");
            Console.Write("Choice: ");
            var choice = Console.ReadLine();

            if (choice != "1" && choice != "2")
            {
                Console.WriteLine("Invalid choice. Please select 1 or 2.");
                Console.Write("Press Enter to continue...");
                Console.ReadLine();
                return;
            }

            Console.Write("Name: ");
            var name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty.");
                Console.Write("Press Enter to continue...");
                Console.ReadLine();
                return;
            }

            Console.Write("Weight: ");
            if (!double.TryParse(Console.ReadLine(), out var weight) || weight < 0)
            {
                Console.WriteLine("Invalid weight. Please enter a valid positive number.");
                Console.Write("Press Enter to continue...");
                Console.ReadLine();
                return;
            }

            Console.Write("Value: ");
            if (!int.TryParse(Console.ReadLine(), out var value) || value < 0)
            {
                Console.WriteLine("Invalid value. Please enter a valid positive integer.");
                Console.Write("Press Enter to continue...");
                Console.ReadLine();
                return;
            }

            if (choice == "1")
            {
                Console.Write("Damage: ");
                if (!int.TryParse(Console.ReadLine(), out var damage) || damage < 0)
                {
                    Console.WriteLine("Invalid damage. Please enter a valid positive integer.");
                    Console.Write("Press Enter to continue...");
                    Console.ReadLine();
                    return;
                }

                Console.Write("Fire Rate: ");
                if (!double.TryParse(Console.ReadLine(), out var fireRate) || fireRate < 0)
                {
                    Console.WriteLine("Invalid fire rate. Please enter a valid positive number.");
                    Console.Write("Press Enter to continue...");
                    Console.ReadLine();
                    return;
                }

                await ItemService.AddItem(new Weapon
                {
                    Name = name,
                    Weight = weight,
                    Value = value,
                    Category = Category.Weapon,
                    Damage = damage,
                    FireRate = fireRate
                });
            }
            else if (choice == "2")
            {
                Console.Write("Effect Boost: ");
                if (!double.TryParse(Console.ReadLine(), out var effectBoost) || effectBoost < 0)
                {
                    Console.WriteLine("Invalid effect boost. Please enter a valid positive number.");
                    Console.Write("Press Enter to continue...");
                    Console.ReadLine();
                    return;
                }

                Console.Write("Duration: ");
                if (!double.TryParse(Console.ReadLine(), out var duration) || duration < 0)
                {
                    Console.WriteLine("Invalid duration. Please enter a valid positive number.");
                    Console.Write("Press Enter to continue...");
                    Console.ReadLine();
                    return;
                }

                await ItemService.AddItem(new PowerUp
                {
                    Name = name,
                    Weight = weight,
                    Value = value,
                    Category = Category.PowerUp,
                    EffectBoost = effectBoost,
                    Duration = duration
                });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError adding item: {ex.Message}");
        }
        finally
        {
            Console.Write("Press Enter to continue...");
            Console.ReadLine();
        }
    }

    private static async Task ViewItemsMenu()
    {
        try
        {
            var items = await ItemService.GetItems();
            Console.WriteLine("\n=== All Items ===");
            if (items.Count == 0)
            {
                Console.WriteLine("No items found.");
            }
            else
            {
                foreach (var item in items)
                {
                    item.Display();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError retrieving items: {ex.Message}");
        }
        finally
        {
            Console.Write("Press Enter to continue...");
            Console.ReadLine();
        }
    }

    private static async Task UpdateItemMenu()
    {
        try
        {
            // First, show all items
            var items = await ItemService.GetItems();
            if (items.Count == 0)
            {
                Console.WriteLine("\nNo items to update.");
                Console.Write("Press Enter to continue...");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("\n=== All Items ===");
            foreach (var item in items)
            {
                item.Display();
            }

            Console.Write("\nEnter Item ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("Invalid ID. Please enter a valid integer.");
                Console.Write("Press Enter to continue...");
                Console.ReadLine();
                return;
            }
            
            var itemToUpdate = await ItemService.GetItemById(id);
            if (itemToUpdate == null)
            {
                Console.WriteLine("Item not found.");
                Console.Write("Press Enter to continue...");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("\nCurrent item:");
            itemToUpdate.Display();

            // Update common fields
            Console.Write("\nEnter new name (or press Enter to keep current): ");
            var name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
                itemToUpdate.Name = name;

            Console.Write("Enter new weight (or press Enter to keep current): ");
            var weightInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(weightInput))
            {
                if (double.TryParse(weightInput, out var weight) && weight >= 0)
                    itemToUpdate.Weight = weight;
                else
                    Console.WriteLine("Invalid weight, keeping current value.");
            }

            Console.Write("Enter new value (or press Enter to keep current): ");
            var valueInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(valueInput))
            {
                if (int.TryParse(valueInput, out var value) && value >= 0)
                    itemToUpdate.Value = value;
                else
                    Console.WriteLine("Invalid value, keeping current value.");
            }

            // Update specific fields based on type
            if (itemToUpdate is Weapon weapon)
            {
                Console.Write("Enter new damage (or press Enter to keep current): ");
                var damageInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(damageInput))
                {
                    if (int.TryParse(damageInput, out var damage) && damage >= 0)
                        weapon.Damage = damage;
                    else
                        Console.WriteLine("Invalid damage, keeping current value.");
                }

                Console.Write("Enter new fire rate (or press Enter to keep current): ");
                var fireRateInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(fireRateInput))
                {
                    if (double.TryParse(fireRateInput, out var fireRate) && fireRate >= 0)
                        weapon.FireRate = fireRate;
                    else
                        Console.WriteLine("Invalid fire rate, keeping current value.");
                }
            }
            else if (itemToUpdate is PowerUp powerUp)
            {
                Console.Write("Enter new effect boost (or press Enter to keep current): ");
                var boostInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(boostInput))
                {
                    if (double.TryParse(boostInput, out var boost) && boost >= 0)
                        powerUp.EffectBoost = boost;
                    else
                        Console.WriteLine("Invalid effect boost, keeping current value.");
                }

                Console.Write("Enter new duration (or press Enter to keep current): ");
                var durationInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(durationInput))
                {
                    if (double.TryParse(durationInput, out var duration) && duration >= 0)
                        powerUp.Duration = duration;
                    else
                        Console.WriteLine("Invalid duration, keeping current value.");
                }
            }

            await ItemService.UpdateItem(itemToUpdate);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError updating item: {ex.Message}");
        }
        finally
        {
            Console.Write("Press Enter to continue...");
            Console.ReadLine();
        }
    }

    private static async Task DeleteItemMenu()
    {
        try
        {
            var items = await ItemService.GetItems();
            if (items.Count == 0)
            {
                Console.WriteLine("\nNo items to delete.");
                Console.Write("Press Enter to continue...");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("\n=== All Items ===");
            foreach (var item in items)
            {
                item.Display();
            }

            Console.Write("\nEnter Item ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("Invalid ID. Please enter a valid integer.");
                Console.Write("Press Enter to continue...");
                Console.ReadLine();
                return;
            }

            await ItemService.DeleteItem(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError deleting item: {ex.Message}");
        }
        finally
        {
            Console.Write("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}
