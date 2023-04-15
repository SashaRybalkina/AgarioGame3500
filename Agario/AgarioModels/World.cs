using System;
using System.Linq;
using System.Numerics;
using FileLogger;

namespace AgarioModels;
public class World
{
    public readonly double Width;
    public readonly double Height;
    public List<Player> players;
    public Player player;
    public List<Food> foods;
    public Food food;
    public CustomFileLogger logger;
    public long playerID;

    public World()
    {
        this.Width = 5000;
        this.Height = 5000;
        player = new Player("A", 1000, 1000, 100, 120, 1000);
        foods = new List<Food>();
    }
}