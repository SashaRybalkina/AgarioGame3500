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
        players = new List<Player>();
        foods = new List<Food>();
        player = new Player(1, "A", 100, 100, 120, 0);
        IDs = new List<long>();       
    }

    public void AdvanceGameOneStep()
    {
        ///if (foods.Count < 15)
        ///{
            ///Random random = new Random();
            ///long id = random.Next(1000);
            ///while(IDs.Contains(id))
            ///{
                ///id = random.Next(1000);
            ///}
            ///IDs.Add(id);
            ///foods.Add(new Food(id, new Vector2(random.Next(0,(int)this.Width), random.Next(0, (int)this.Height)), random.Next(50, 130), random.Next(100)));
        ///}
        //foreach (Food food in foods)
        //{
        //    if (player.location == food.location)
        //    {
        //        player.Mass += food.Mass;
        //        foods.Remove(food);            
        //    }
        //}
    }
}