namespace asteroidsbackend.Models;

public class Weapon : GameItem
{
    public int Damage { get; set; }
    public double FireRate { get; set; }

    public override void Display()
    {
        Console.WriteLine($"{Id} - {Name} [Weapon] | DMG: {Damage} | FireRate: {FireRate}");
    }
}
