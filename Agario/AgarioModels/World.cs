using System.Numerics;
using FileLogger;

namespace AgarioModels;
public class World
{
    public readonly double Width;
    public readonly double Height;
    public List<Player> players;
    public List<Food> foods;
    public CustomFileLogger logger;

    public World()
    {
        this.Width = 5000;
        this.Height = 5000;       
        players = new List<Player>(3);
        foods = new List<Food>(15);
    }

    //public void AdvanceGameOneStep()
    //{
    //    lock (this)
    //    {
    //        X += Direction.X;
    //        Y += Direction.Y;
    //        if (X == 800 || Y == 800 || X == 0 || Y == 0)
    //        {
    //            if (X == 800 || X == 0)
    //                Direction = new Vector2(-Direction.X, Direction.Y);
    //            if (Y == 800 || Y == 0)
    //                Direction = new Vector2(Direction.X, -Direction.Y);
    //        }
    //    }
    //}

}

