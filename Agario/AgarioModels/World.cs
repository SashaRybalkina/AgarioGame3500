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
    public Vector2 direction;
    private List<long> IDs;

    public World()
    {
        this.Width = 5000;
        this.Height = 5000;       
        players = new List<Player>(3);
        foods = new List<Food>(15);
        player = new Player(1, "A", new Vector2(400, 400), 12, 0);
        direction = new Vector2(10, 5);
        IDs = new List<long>();
        int i = 1;
        while (i < 16)
        {
            Random random = new Random();
            IDs.Add(i);
            foods.Add(new Food(i, new Vector2(random.Next(0, 5000), random.Next(0, 5000)), random.Next(0, 13), random.Next(100)));
            i++;
        }
    }

    public void AdvanceGameOneStep()
    {
        lock (this)
        {
            player.x += direction.X;
            player.y += direction.Y;
            if (player.x == 800 || player.y == 800 || player.x == 0 || player.y == 0)
            {
                if (player.x == 800 || player.x == 0)
                    direction = new Vector2(-direction.X, direction.Y);
                if (player.y == 800 || player.y == 0)
                    direction = new Vector2(direction.X, -direction.Y);
            }
        }
        if (foods.Count < 15)
        {
            Random random = new Random();
            long id = random.Next(1000);
            while(IDs.Contains(id))
            {
                id = random.Next(1000);
            }
            IDs.Add(id);
            foods.Add(new Food(id, new Vector2(random.Next(0,5000), random.Next(0, 5000)), random.Next(0, 13), random.Next(100)));
        }
    }

}

