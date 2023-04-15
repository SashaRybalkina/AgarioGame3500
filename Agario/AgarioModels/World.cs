using FileLogger;

namespace AgarioModels;

/// <summary>
/// Author:    Aurora Zuo 
/// Partner:   Sasha Rybalkina
/// Date:      14-Apr-2023
/// Course:    CS 3500, University of Utah, School of Computing
/// Copyright: CS 3500, Aurora Zuo, and Zhuofei Lyu - This work not 
///            be copied for use in Academic Coursework.
///            
/// Aurora Zuo and Sasha Rybalkina certify that we wrote this code from scratch and
/// did not copy it in part or whole from another source. All 
/// references used in the completion of the assignments are cited 
/// in our README file.
/// 
/// File Content
///	    The World is responsible for storing the current state of the game,
///	    including the status and location of every object in the game.
/// </summary>
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

    /// <summary>
    /// Defalut constructor that initializes the status of the game and
    /// game objects.
    /// </summary>
    public World()
    {
        this.Width = 5000;
        this.Height = 5000;
        player = new Player("A", 1000, 1000, 100, 120, 1000);
        foods = new List<Food>();
    }
}