using asteroidsbackend.Models;
using asteroidsbackend.Models.Interfaces;

namespace asteroidsbackend.Data
{
    public static class GameDatabase
    {
        public static List<IItem> Items { get; private set; } = new List<IItem>();
        public static int NextId = 1;

        // Concurrency guard
        public static readonly SemaphoreSlim DbLock = new SemaphoreSlim(1, 1);
    }
}
