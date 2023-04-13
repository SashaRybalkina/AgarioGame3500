using System;
using System.Numerics;
using System.Text.Json.Serialization;

namespace AgarioModels
{
	public class Player : GameObject
	{
        public string Name { get; set; }

        [JsonConstructor]
        public Player(string Name, float X, float Y, int ARGBColor, long ID, float Mass)
            : base (ID, X, Y, Mass, ARGBColor)
        {
            this.Name = Name;            
        }     
        
	}
}

