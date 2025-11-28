using asteroidsbackend.Models;
using asteroidsbackend.Data;
using System.Runtime.InteropServices.Marshalling;

namespace asteroidsbackend.Services;

public static class AnalysisService
{
    public static void ShowCategoryCounts()
    {
        Console.Clear();

        var counts = GameDatabase.Items
            .GroupBy(i => i.Category)
            .Select(g => new { Category = g.Key, Count = g.Count() });

        foreach (var c in counts)
            Console.WriteLine($"{c.Category}: {c.Count}");
        
        Console.ReadLine();
    }

    public static void ShowAverages()
    {
        Console.Clear();
        var weapons = GameDatabase.Items.OfType<Weapon>();
        var powerups = GameDatabase.Items.OfType<PowerUp>();

        Console.WriteLine("Average Weapon Damage: " + weapons.Average(w => w.Damage));
        Console.WriteLine("Average PowerUp Duration: " + powerups.Average(p => p.Duration));
        Console.ReadLine();
    }

    public static void ShowTop5Weapons()
    {
        Console.Clear();
        var top = GameDatabase.Items
            .OfType<Weapon>()
            .OrderByDescending(w => w.Damage)
            .Take(5);

        foreach (var w in top)
            w.Display();

        Console.ReadLine();
    }
}