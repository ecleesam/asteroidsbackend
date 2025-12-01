namespace asteroidsbackend.Models.Interfaces
{
    public interface IItem
    {
        int Id { get; set; }
        string Name { get; set; }
        double Weight { get; set; }
        int Value { get; set; }
        Category Category { get; set; }
        void Display();
    }
}
