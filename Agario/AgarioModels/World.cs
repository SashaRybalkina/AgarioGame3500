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
    public Vector2 Direction;
    public long[] eaten;
    public List<Food> foods;
    public Food food;
    public CustomFileLogger logger;
    public long playerID;
    private List<long> IDs;

    public World()
    {
        this.Width = 5000;
        this.Height = 5000;
        player = new Player("A", 1000, 1000, 100, 120, 1000);
    }

    public void AdvanceGameOneStep()
    {
        //player.X += Direction.X/1000;
        //player.Y += Direction.Y/1000;

        //if (foods.Count < 15)
        //{
        //    Random random = new Random();
        //    long id = random.Next(1000);
        //    while (IDs.Contains(id))
        //    {
        //        id = random.Next(1000);
        //    }
        //    IDs.Add(id);
        //    foods.Add(new Food(id, new Vector2(random.Next(0, (int)this.Width), random.Next(0, (int)this.Height)), random.Next(50, 130), random.Next(100)));
        //}
        //foreach (Food food in foods)
        //{
        //    if (player.X == food.X && player.Y == food.Y)
        //    {
        //        player.Mass += food.Mass;
        //        foods.Remove(food);
        //    }
        //}
    }
}