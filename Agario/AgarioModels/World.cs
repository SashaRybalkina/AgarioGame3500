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
    private List<long> IDs;

    public World()
    {
        this.Width = 500;
        this.Height = 500;       
        players = new List<Player>(3);
        foods = new List<Food>(15);
        player = new Player(1, "A", new Vector2(100, 100), 120, 0);
        IDs = new List<long>();
        int id = 1;
        while (id < 16)
        {
            Random random = new Random();
            IDs.Add(id);
            foods.Add(new Food(id, new Vector2(random.Next(0, (int)this.Width), random.Next(0, (int)this.Height)), random.Next(0, 13), random.Next(100)));
            id++;
        }
    }

    public void AdvanceGameOneStep()
    {
        player.x = 
        if (foods.Count < 15)
        {
            Random random = new Random();
            long id = random.Next(1000);
            while(IDs.Contains(id))
            {
                id = random.Next(1000);
            }
            IDs.Add(id);
            foods.Add(new Food(id, new Vector2(random.Next(0,(int)this.Width), random.Next(0, (int)this.Height)), random.Next(50, 130), random.Next(100)));
        }
        foreach (Food food in foods)
        {
            if (player.location == food.location)
            {
                foods.Remove(food);
                player.Mass *= 2;
            }
        }
    }

}