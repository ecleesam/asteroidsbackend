using asteroidsbackend.Models.Interfaces;

namespace asteroidsbackend.Models
{
    public class PowerUp : BaseItem, IPowerUp
    {
        public double Duration { get; set; }
        public double EffectBoost { get; set; }

        public override void Display()
        {
            Console.WriteLine(
                $"{Id} - {Name} [PowerUp] | Duration:{Duration}s | Boost:{EffectBoost} | Val:{Value} | W:{Weight}");
        }
    }
}
