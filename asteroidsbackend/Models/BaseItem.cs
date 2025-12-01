using asteroidsbackend.Models.Interfaces;

namespace asteroidsbackend.Models
{
    public abstract class BaseItem : IItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public double Weight { get; set; }
        public int Value { get; set; }
        public Category Category { get; set; }

        public abstract void Display();
    }
}
