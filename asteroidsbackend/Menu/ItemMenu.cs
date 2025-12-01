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
        Console.WriteLine("\n1. Add Weapon");
        Console.WriteLine("2. Add PowerUp");
        Console.Write("Choice: ");
        var choice = Console.ReadLine();

        Console.Write("Name: ");
        var name = Console.ReadLine() ?? "";
        Console.Write("Weight: ");
        var weight = double.Parse(Console.ReadLine() ?? "0");
        Console.Write("Value: ");
        var value = int.Parse(Console.ReadLine() ?? "0");

        if (choice == "1")
        {
            Console.Write("Damage: ");
            var damage = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Fire Rate: ");
            var fireRate = double.Parse(Console.ReadLine() ?? "0");

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
            var effectBoost = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Duration: ");
            var duration = double.Parse(Console.ReadLine() ?? "0");

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
        Console.Write("Press Enter to continue...");
        Console.ReadLine();
    }

    private static async Task ViewItemsMenu()
    {
        var items = await ItemService.GetItems();
        Console.WriteLine("\n=== All Items ===");
        foreach (var item in items)
        {
            item.Display();
        }
        Console.Write("Press Enter to continue...");
        Console.ReadLine();
    }

    private static async Task UpdateItemMenu()
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
        var id = int.Parse(Console.ReadLine() ?? "0");
        
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
            itemToUpdate.Weight = double.Parse(weightInput);

        Console.Write("Enter new value (or press Enter to keep current): ");
        var valueInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(valueInput))
            itemToUpdate.Value = int.Parse(valueInput);

        // Update specific fields based on type
        if (itemToUpdate is Weapon weapon)
        {
            Console.Write("Enter new damage (or press Enter to keep current): ");
            var damageInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(damageInput))
                weapon.Damage = int.Parse(damageInput);

            Console.Write("Enter new fire rate (or press Enter to keep current): ");
            var fireRateInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(fireRateInput))
                weapon.FireRate = double.Parse(fireRateInput);
        }
        else if (itemToUpdate is PowerUp powerUp)
        {
            Console.Write("Enter new effect boost (or press Enter to keep current): ");
            var boostInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(boostInput))
                powerUp.EffectBoost = double.Parse(boostInput);

            Console.Write("Enter new duration (or press Enter to keep current): ");
            var durationInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(durationInput))
                powerUp.Duration = double.Parse(durationInput);
        }

        await ItemService.UpdateItem(itemToUpdate);
        Console.Write("Press Enter to continue...");
        Console.ReadLine();
    }

    private static async Task DeleteItemMenu()
    {
        Console.Write("Enter Item ID to delete: ");
        var id = int.Parse(Console.ReadLine() ?? "0");
        await ItemService.DeleteItem(id);
        Console.Write("Press Enter to continue...");
        Console.ReadLine();
    }
}
