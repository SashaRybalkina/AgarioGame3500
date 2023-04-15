using System;
using System.Numerics;
using System.Text.Json.Serialization;

namespace AgarioModels
{
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
    ///		This class contains all the necessary properties for game objects (food and player)
    /// </summary>
	public class GameObject
	{
        public float X { get { return location.X; } set { location.X = value; } }
        public float Y { get { return location.Y; } set { location.Y = value; } }
        public Vector2 location;
        public int ARGBColor { get; set; }
        public float Mass { get; set; }
        public long ID { get; set; }
        public float Radius => MathF.Sqrt(Mass);

        /// <summary>
        /// Default construtor.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Mass"></param>
        /// <param name="ARGBColor"></param>
        [JsonConstructor]
        public GameObject(long ID, float X, float Y, float Mass, int ARGBColor)
		{
            this.ID = ID;
            this.Mass = Mass;
            this.ARGBColor = ARGBColor;
            this.X = X;
            this.Y = Y;
		}
	}
}

