namespace asteroidsbackend.Models;

public class PowerUp : GameItem
{
    public double Duration { get; set; }
    public double EffectBoost { get; set;}

    public override void Display()
    {
        Console.WriteLine($"{Id} - {Name} [PowerUp] | Duration: {Duration}s | Boost: {EffectBoost}");
    }
}