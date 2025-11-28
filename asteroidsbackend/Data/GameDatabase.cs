using asteroidsbackend.Models;

namespace asteroidsbackend.Data;

public static class GameDatabase
{
    public static List<GameItem> Items { get; set; } = new List<GameItem>();
    public static int NextId = 1;
}