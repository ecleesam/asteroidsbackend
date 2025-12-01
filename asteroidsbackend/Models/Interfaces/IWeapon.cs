namespace asteroidsbackend.Models.Interfaces
{
    public interface IWeapon : IItem
    {
        int Damage { get; set; }
        double FireRate { get; set; }
    }
}
