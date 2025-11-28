using asteroidsbackend.Models;
using asteroidsbackend.Data;

namespace asteroidsbackend.Services;

public static class ItemService
{
    public static void AddItem()
    {
        Console.Clear();
        Console.WriteLine("CREATE ITEM");
        Console.WriteLine("1. Weapon");
        Console.WriteLine("2. Power-Up");
        Console.Write("Choice: ");

        string choice = Console.ReadLine();

        if (choice == "1")
            CreateWeapon();
        else if (choice == "2")
            CreatePowerUp();
        else
            Console.WriteLine("Invalid choice.");

        Console.ReadLine();
    }

    private static void CreateWeapon()
    {
        Console.Write("Name: ");
        string name = Console.ReadLine();

        Console.Write("Damage: ");
        int dmg = int.Parse(Console.ReadLine());

        Console.Write("Fire Rate: ");
        double rate = double.Parse(Console.ReadLine());

        Weapon w = new Weapon
        {
            Id = GameDatabase.NextId++,
            Name = name,
            Category = Category.Weapon,
            Damage = dmg,
            FireRate = rate
        };

        GameDatabase.Items.Add(w);
        Console.WriteLine("Weapon added!");
    }

    private static void CreatePowerUp()
    {
        Console.Write("Name: ");
        string name = Console.ReadLine();

        Console.Write("Duration: ");
        double dur = double.Parse(Console.ReadLine());

        Console.Write("Effect Boost: ");
        double boost = double.Parse(Console.ReadLine());

        PowerUp p = new PowerUp
        {
            Id = GameDatabase.NextId++,
            Name = name,
            Category = Category.PowerUp,
            Duration = dur,
            EffectBoost = boost
        };

        GameDatabase.Items.Add(p);
        Console.WriteLine("Power-Up added!");
    }

    public static void ViewItems()
    {
        Console.Clear();
        Console.WriteLine("ALL ITEMS");
        foreach (var item in GameDatabase.Items)
            item.Display();
        Console.ReadLine();
    }

    public static void UpdateItem()
    {
        Console.Clear();
        Console.Write("Enter ID to update: ");
        int id = int.Parse(Console.ReadLine());

        GameItem item = GameDatabase.Items.FirstOrDefault(x => x.Id == id);
        if (item == null) { Console.WriteLine("Not found."); Console.ReadLine(); return; }

        Console.Write("New Name: ");
        item.Name = Console.ReadLine();
        Console.WriteLine("Updated!");
        Console.ReadLine();
    }

    public static void DeleteItem()
    {
        Console.Clear();
        Console.Write("Enter ID to delete: ");
        int id = int.Parse(Console.ReadLine());

        GameItem item = GameDatabase.Items.FirstOrDefault(x => x.Id == id);
        if (item == null) { Console.WriteLine("Not found."); Console.ReadLine(); return; }

        GameDatabase.Items.Remove(item);
        Console.WriteLine("Deleted!");
        Console.ReadLine();
    }   
}
