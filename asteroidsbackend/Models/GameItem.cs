using System.ComponentModel;

namespace asteroidsbackend.Models;

public class GameItem
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public Category Category { get; set; }
    public int Value { get; set; }
    public double Weight{ get; set; }

    public virtual void Display()
    {
        Console.WriteLine($"{Id} - {Name} [{Category}] Value: {Value}, Weight: {Weight}");
    }
}