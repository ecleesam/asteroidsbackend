namespace asteroidsbackend.Models.Interfaces
{
    public interface IPowerUp : IItem
    {
        double Duration { get; set; }
        double EffectBoost { get; set; }
    }
}
